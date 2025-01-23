using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockManagement.Models.StockDTO.UsersDTO
{
    public class AddUpdateUserRequestDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
