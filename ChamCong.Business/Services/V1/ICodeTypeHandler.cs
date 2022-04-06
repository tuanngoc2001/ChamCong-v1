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
        Task<PagedList<Response>> Gettimesheet(Guid Id, int size, int page);
        Task<PagedList<Response>> Searchtimesheet(int size, int page, string search);
        Task<Response> CheckOut(Guid Id, PlanCheckOutViewModel planviewmodel);
        Task<PagedList<Response>> UserBySearch(int size, int page, string search);
        Task<Response> CreateUser(UserViewModel userviewmodel);
    }
}
