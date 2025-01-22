using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockManagement.Models.StockDTO.UsersDTO
{
    public class AddCreateUsersResponse
    {
        public string UserName { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "The Maximum Allowed Length for Password is 8")]
        public string Password { get; set; }
        public string UserRole { get; set; }
    }
}
