using ChamCong.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong.Business.Services.V1
{
    public interface ICodeTypeHandler
    {
        Task<Response> CheckIn(PlanCreateModel planviewmodel);
        Task<PagedList<TimeSheetViewModel>> GetAllTimeSheet(Guid Id, int size, int page);
        Task<PagedList<TimeSheetViewModel>> Searchtimesheet(int size, int page, string search);
        Task<Response> CheckOut(Guid Id, PlanCheckOutViewModel planmodel);
        Task<PagedList<UserViewModel>> UserBySearch(int size, int page, string search);
        Task<Response> CreateUser(Guid IdAdmin,UserViewModel userviewmodel);
    }
}
