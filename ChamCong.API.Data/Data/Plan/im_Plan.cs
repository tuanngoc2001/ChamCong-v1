using ChamCong.API.Data.Data.Profile;
using ChamCong.API.Data.Data.Task;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong.API.Data.Data.Plan
{
    [Table("im_Plan")]
    public class im_Plan
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public bool IsLate { get; set; }

        public float CompletionPercentage { get; set; }
        public int TotalTaskPlannedCount { get; set; }
        public int TotalTaskComplete { get; set; }
        public int TotalTaskOutStandingCount { get; set; }
        public int TotalTimeWorkCount { get; set; }
        public DateTime TimeCheckIn { get; set; }
        public DateTime TimeCheckOut { get; set; }
        public List<im_Task> TaskListCode { get; set; }

        public Guid? UserId { get; set; }
        [ForeignKey("UserId")]
        public im_User user { get; set; }

    }
}

