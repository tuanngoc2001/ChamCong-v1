using ChamCong.Business.Services.V1;
using ChamCong.Common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChamCong.API.v1.Controllers.V1
{
    [Route("api/timesheets")]
    [ApiController]
    public class TimeSheetController : ControllerBase
    {
        private readonly ICodeTypeHandler _repository;
        public TimeSheetController(ICodeTypeHandler repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// Qúa trình check in
        /// </summary>
        /// <param name="planmodel"></param>
        /// <returns></returns>
        [HttpPost("checkin")]
        [Authorize]
        public async Task<ActionResult> CheckIn(PlanCreateModel planmodel)
        {
                var result = await _repository.CheckIn(planmodel);
                return Helper.TransformData(result) ;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="plancheckoutmodel"></param>
        /// <returns></returns>
        [HttpPut("{id}/checkout")]
        [Authorize]
        public async Task<ActionResult> Checkout(Guid Id, [FromBody] PlanCheckOutViewModel plancheckoutmodel)
        {
            var result = await _repository.CheckOut(Id, plancheckoutmodel);
            return Helper.TransformData(result);
            
        }
        /// <summary>
        /// GET timesheet
        /// </summary>
        /// <param name="size"></param>
        /// <param name="page"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult>GetTimeSheet(int size,int page,string search)
        {
            var result = await _repository.Searchtimesheet(size, page, search);
            return Helper.TransformData(new Response<PagedList<TimeSheetViewModel>>(result));

        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateUser(Guid Id,UserViewModel userviewmodel)
        {
            var result = await _repository.CreateUser(Id, userviewmodel);
            return Helper.TransformData(result);
        }
    }
}
