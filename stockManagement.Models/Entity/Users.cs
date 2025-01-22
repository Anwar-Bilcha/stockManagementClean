using stockManagement.Models.Enums;
using stockManagement.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockManagement.Models.Entity
{
    public class Users :EntityParent
    {
        [Key]
        public string UserName { get; set; }
        [Required]
        [MinLength(8, ErrorMessage ="The Maximum Allowed Length for Password is 8")]
        public string Password { get; set; }
        public string PasswordHashed { get; set; }
        public string UserRole { get; set; } 
    }
}
