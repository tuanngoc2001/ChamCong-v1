using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong.Business.Services.V1
{
    public class AccountModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class UserViewModel
    {
        public string EmplyeeId { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
        public DateTime LastLoginDate { get; set; }
    }

    public class PlanCreateModel
    {
        public Guid Id { get; set; }
        public List<TaskCreate> PlanCodeList { get; set; }
    }
    public class TaskCreate
    {
        public string Title { get; set; }
    }

    public class PlanCheckOutViewModel
    {
        //id của người dùng
        public Guid Id { get; set; }
        public List<TaskCheckOutViewModel> PlanList { get; set; }
    }
    public class TaskCheckOutViewModel
    {
        public string Title { get; set; }
        public string? Note { get; set; }

        public bool IsComplete { get; set; }
    }
    public class TimeSheetViewModel
    {
        public Guid Id { get; set; }
        public float CompletionPercentage { get; set; }
        public int TotalTaskPlannedCount { get; set; }
        public int TotalTaskComplete { get; set; }
        public int TotalTaskOutStandingCount { get; set; }
        public int TotalTimeWorkCount { get; set; }
        public DateTime TimeCheckIn { get; set; }
        public DateTime TimeCheckOut { get; set; }
    }
    //public class APIReponsitory
    //{
    //    public bool Success { get; set; }
    //    public string Message { get; set; }
    //    public object Data { get; set; }
    //}
}
