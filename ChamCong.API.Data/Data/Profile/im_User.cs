using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong.API.Data.Data.Profile
{
    [Table("im_User")]
    public class im_User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string EmplyeeId { get; set; }
        [MaxLength(50)]
        [Required]
        public string UserName { get; set; }
        [MaxLength(50)]
        [Required]
        public string PassWord { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime LastLoginDate { get; set; }
        [ForeignKey("GroudID")]
        public virtual im_User_Group UserGroup { get; set; }
        public Guid GroudID { get; set; }
    }
}
