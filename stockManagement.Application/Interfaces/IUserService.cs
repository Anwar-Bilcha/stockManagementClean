using stockManagement.Models.Entity;
using stockManagement.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockManagement.Application.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<Users>> CreateUsersAsync(Users user);
        Task<ApiResponse<List<Users>>> CreateBulkUsersAsync(List<Users> users);
        Task<ApiResponse<Users>> UpdateUsersAsync(Users user);
        Task<ApiResponse<Users>> DeleteUsersAsync(Users users);
        ApiResponse<Users> GetUser(string username);
        ApiResponse<List<Users>> GetUsers();
    }
}
