using AutoMapper;
using ChamCong.API.Data.Data;
using ChamCong.API.Data.Data.Plan;
using ChamCong.API.Data.Data.Profile;
using ChamCong.API.Data.Data.Task;
using ChamCong.Common.Constant;
using ChamCong.Common.Utils;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong.Business.Services.V1
{
    public class CodeTypeHandler : ICodeTypeHandler
    {
        private readonly ImDbContext _dbContext;
        private readonly IMapper _mapper;

        public CodeTypeHandler(ImDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response> CheckIn(PlanCreateModel planviewmodel)
        {
            try
            {
                //kiểm tra xem id user có đúng hay k
                var getuser = _dbContext.im_User.SingleOrDefault(p => p.Id == planviewmodel.Id);
                if (getuser != null)
                {
                    //khởi tạo một plan
                    var plan = new im_Plan()
                    {
                        Id = new Guid(),
                        TimeCheckIn = DateTime.Now,
                        UserId = planviewmodel.Id,
                    };
                    //kiểm tra xem check in có muộn hay không
                    if (DateTime.Now.Hour >= 9)
                        plan.IsLate = true;
                    else
                        plan.IsLate = false;

                    plan.TaskListCode = new List<im_Task>();
                    foreach (var item in planviewmodel.PlanCodeList)
                    {
                        var task = new im_Task()
                        {
                            //mặc định sẵn kiểu là planned khi checkout sẽ kiểm tra lại 
                            Id = new Guid(),
                            Title = item.Title,
                            TypeTask = StatusTask.Planned,
                            Note = "",
                            IsComplete = false,
                        };
                        plan.TaskListCode.Add(task);
                    }
                    _dbContext.im_Plan.Add(plan);
                    await _dbContext.SaveChangesAsync();
                    var data = _mapper.Map<PlanCreateModel>(plan);
                    return new Response<PlanCreateModel>(data);
                }
                //kiểm tra xem đã check in hay chưa

                else
                {
                    Log.Error($"Not found user {planviewmodel.Id}");
                    return new Response<PlanCreateModel>
                    {
                        Data = null,
                        Message = $"Not found id {planviewmodel.Id}",

                    };
                }
            }
            catch (Exception e)
            {
                Log.Error(e, $"Cannot check in {planviewmodel}");
                return new Response<PlanCreateModel>
                {
                    Data = null,
                    Message = e.Message,
                    Code = HttpStatusCode.InternalServerError
                };
            };
        }

        public async Task<Response> CheckOut(Guid Id, PlanCheckOutViewModel planmodel)
        {
            try
            {
                //kiểm tra xem có người dùng này k
                var user = await _dbContext.im_User.SingleOrDefaultAsync(p => p.Id == Id);
                if (user == null)
                {
                    Log.Error($"No user has id:{Id}");
                    return null;
                }
                //kiểm tra xem đã check in chưa
                var plancheckout = await _dbContext.im_Plan.Where(p => p.UserId == Id && p.TimeCheckIn.Date == DateTime.Now.Date).FirstOrDefaultAsync();
                if (plancheckout == null)
                {
                    Log.Error($"{Id} Not checked in yet");
                    return null;
                }
                //kiểm tra xem đã làm đủ 8h chưa
                if (DateTime.Now.Subtract(plancheckout.TimeCheckIn).Hours < 8)
                {
                    Log.Error($"{Id} not done enough 8h");
                    return null;
                }
                plancheckout.TimeCheckOut = DateTime.Now;
                foreach (var item in planmodel.PlanList)
                {
                    //kiểm tra xem task đã có chưa
                    //nếu có rồi thì cập nhật 
                    //chua có thì them vào đồg thời type sẽ là outstanding
                    var taskcheckout = _dbContext.im_Task.Where(p => p.Title.ToLower().Trim() == item.Title.ToLower().Trim()).FirstOrDefault();
                    if (taskcheckout == null)
                    {
                        var tasknew = new im_Task()
                        {
                            Id = new Guid(),
                            Title = item.Title,
                            PlanId = plancheckout.Id,
                            TypeTask = StatusTask.Outstanding,
                            IsComplete = item.IsComplete
                        };
                        if (item.IsComplete == true)//đã hoàn thành
                            tasknew.Note = "";
                        else
                            tasknew.Note = item.Note;
                        await _dbContext.im_Task.AddAsync(tasknew);

                    }    //th nếu có rồi thì cập nhật lại
                    else
                    {

                        taskcheckout.IsComplete = item.IsComplete;
                        if (taskcheckout.IsComplete)
                        {
                            taskcheckout.Note = "";
                        }
                        else
                            taskcheckout.Note = item.Note;
                    }
                }
                //cập nhật trước
                await _dbContext.SaveChangesAsync();
                // cập nhật timesheet 
                plancheckout.TotalTimeWorkCount = DateTime.Now.Subtract(plancheckout.TimeCheckIn).Hours;
                plancheckout.TotalTaskPlannedCount = _dbContext.im_Task.Where(p => p.TypeTask == StatusTask.Planned && p.PlanId == plancheckout.Id).Count();
                plancheckout.TotalTaskOutStandingCount = _dbContext.im_Task.Where(p => p.TypeTask == StatusTask.Outstanding && p.PlanId == plancheckout.Id).Count();
                plancheckout.TotalTaskComplete = _dbContext.im_Task.Where(p => p.IsComplete == true && p.PlanId == plancheckout.Id).Count();
                //ép kiểu
                plancheckout.CompletionPercentage = (float)(plancheckout.TotalTaskComplete / ((plancheckout.TotalTaskPlannedCount + plancheckout.TotalTaskPlannedCount) * 1.0));

                await _dbContext.SaveChangesAsync();
                //var data = _mapper.Map<PlanCheckOutViewModel>(PlanViewModel);


                return new Response<PlanCheckOutViewModel>(planmodel);
            }
            catch (Exception e)
            {
                Log.Error(e, "checkout error");
                return new ResponseError(HttpStatusCode.InternalServerError, e.ToString());
            }
        }
        public async Task<Response> CreateUser(Guid IdAdmin, UserViewModel userviewmodel)
        {
            try
            {
                //chỉ có admin mới có quyền thêm nhân viên mới

                var checkuser = _dbContext.im_User.SingleOrDefault(p => p.Id == IdAdmin);
                if (checkuser != null)
                {
                    var checkrole = _dbContext.im_User_Group.Where(p => p.Id == checkuser.Id && p.Name == RoleConstant.ADMIN_GROUP || p.Name == RoleConstant.MOD_GROUP);
                    if (checkrole == null)
                    {
                        return new Response<UserViewModel>
                        {
                            Data = null,
                            Message = "NO RIGHTS TO CREATE NEW",
                        };
                    }
                    else
                    {
                        var user = new im_User()
                        {
                            Id = new Guid(),
                            EmplyeeId = userviewmodel.EmplyeeId,
                            UserName = userviewmodel.UserName,
                            PassWord = userviewmodel.PassWord,
                            Phone = userviewmodel.Phone,
                            Email = userviewmodel.Email,
                        };
                        await _dbContext.AddAsync(user);
                        await _dbContext.SaveChangesAsync();
                        var data = _mapper.Map<im_User, UserViewModel>(user);
                        return new Response<UserViewModel>(data);
                    }
                }
                else
                {
                    return new ResponseError(HttpStatusCode.MethodNotAllowed, "error");
                }
                //return userviewmodel;


            }
            catch (Exception e)
            {
                Log.Error(e, "Create a user No curved bar ");
                return new ResponseError(HttpStatusCode.ExpectationFailed, e.ToString());
            }
        }

        public async Task<PagedList<TimeSheetViewModel>> GetAllTimeSheet(Guid Id, int size, int page)
        {
            try
            {
                //kiểm tra xem id admin đưa vào có hợp lệ k
                var checkiduser = await _dbContext.im_User.SingleOrDefaultAsync(p => p.Id == Id);
                if (checkiduser != null)
                {
                    var listplans = _dbContext.im_Plan.Where(p => p.UserId == Id);
                    //chuyển đổi danh sách
                    var timesheetListCode = listplans.Select(emp => _mapper.Map<im_Plan, TimeSheetViewModel>(emp));

                    return PagedList<TimeSheetViewModel>.ToPagedList(timesheetListCode, page, size);
                }
                else
                {
                    Log.Error($"No user with id ={Id}");
                    return null;
                }
            }
            catch (Exception e)
            {
                Log.Error(e, $"Unable to retrieve information { Id}");
                return null;
            }
        }

        public async Task<PagedList<TimeSheetViewModel>> Searchtimesheet(int size, int page, string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var user = _dbContext.im_User.Where(p => p.UserName.ToLower().Trim().Contains(search.ToLower().Trim()));
                if (user != null)
                {
                    var timesheetplan = from a in _dbContext.im_User
                                        join b in _dbContext.im_Plan
                                        on a.Id equals b.UserId
                                        where a.UserName.Contains(search.Trim())
                                        select new TimeSheetViewModel()
                                        {
                                            Id = b.Id,
                                            CompletionPercentage = b.CompletionPercentage,
                                            TotalTaskPlannedCount = b.TotalTaskPlannedCount,
                                            TotalTaskComplete = b.TotalTaskComplete,
                                            TotalTaskOutStandingCount = b.TotalTaskOutStandingCount,
                                            TotalTimeWorkCount = b.TotalTimeWorkCount,
                                            TimeCheckIn = b.TimeCheckIn,
                                            TimeCheckOut = b.TimeCheckOut,
                                        };

                    return PagedList<TimeSheetViewModel>.ToPagedList(timesheetplan, page, size);
                }
                else
                {
                    Log.Error("There are no records");
                    return null;
                }
            }
            else
            {
                //nếu k search thì lấy ra hết theo ngày
                var listplan = _dbContext.im_Plan.OrderBy(p => p.TimeCheckOut);
                var timesheetListCode = listplan.Select(emp => _mapper.Map<im_Plan, TimeSheetViewModel>(emp));
                return PagedList<TimeSheetViewModel>.ToPagedList(timesheetListCode, page, size);

            }

            
        }
        

        public async Task<PagedList<UserViewModel>> UserBySearch(int size, int page, string search)
        {
            var userpagging = _dbContext.im_User.Where(p => p.UserName.ToLower().Trim().Contains(search.ToLower().Trim())).AsQueryable();
            var userviewmodel = userpagging.Select(emp => _mapper.Map<im_User, UserViewModel>(emp));
            return PagedList<UserViewModel>.ToPagedList(userviewmodel, page, size);
        }
    }
}
