using ChamCong.API.Data.Data.Plan;
using ChamCong.Common.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong.API.Data.Data.Task
{
    [Table("im_Task")]
    public class im_Task
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Title { get; set; }
        [MaxLength(50)]

        public string? Note { get; set; }
        [Required]
        public StatusTask TypeTask { get; set; }
        [Required]
        public bool IsComplete { get; set; }


        public Guid? PlanId { get; set; }
        [ForeignKey("PlanId")]
        public im_Plan Plan { get; set; }
    }
}
