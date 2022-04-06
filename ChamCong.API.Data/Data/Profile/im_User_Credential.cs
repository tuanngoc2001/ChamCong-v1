using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong.API.Data.Data.Profile
{
    [Table("im_User_Credential")]
    public class im_User_Credential
    {
        [Required]
        [MaxLength(50)]
        public string UserGroupId { get; set; }
        [MaxLength(50)]
        [Required]
        public string UserRoleId { get; set; }
        
    }
}
