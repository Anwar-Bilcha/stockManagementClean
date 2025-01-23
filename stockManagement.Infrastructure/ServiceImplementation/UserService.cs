using stockManagement.Application.Interfaces;
using stockManagement.Models.Entity;
using stockManagement.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockManagement.Infrastructure.ServiceImplementation
{
    public class UserService : IUserService
    {
        private readonly StockDbContext _context;

        public UserService(StockDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse<List<Users>>> CreateBulkUsersAsync(List<Users> users)
        {
            ApiResponse<List<Users>> apiResponse = new();
            List<Users> Data = new List<Users>();
            foreach (Users user in users) 
            {
                try
                {
                    var result = _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    Data.Add(user);
                }
                catch (Exception ex)
                {
                    apiResponse.errorMessage += ex.InnerException.Message;
                    apiResponse.isSuccessfullyCompleted = false;
                    apiResponse.Data = new();
                    return apiResponse;
                }
            }
            apiResponse.isSuccessfullyCompleted = true;
            apiResponse.errorMessage = "";
            apiResponse.Data = Data;
            return apiResponse;
        }

        public async Task<ApiResponse<Users>> CreateUsersAsync(Users user)
        {
            ApiResponse<Users> apiResponse = new();
            try
            {
                var result = _context.Users.Add(user);
                await _context.SaveChangesAsync();
                apiResponse.Data = user;
                apiResponse.isSuccessfullyCompleted = true;
                apiResponse.errorMessage = "";
                return apiResponse;
            }
            catch (Exception ex) 
            {
                apiResponse.errorMessage += ex.InnerException.Message;
                apiResponse.isSuccessfullyCompleted = false;
                apiResponse.Data = new();
                return apiResponse;
            }

        }

        public async Task<ApiResponse<Users>> DeleteUsersAsync(Users users)
        {
            var apiResponse = new ApiResponse<Users>();
            var res = _context.Users.Find(users.UserName);
            if (res != null) 
            {
                apiResponse.errorMessage = $"No Such User with Provided {nameof(users.UserName)} {users.UserName} is found in the table.";
                apiResponse.isSuccessfullyCompleted = false;
                apiResponse.Data = new();
                return apiResponse;
            }
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            apiResponse.Data = res;
            apiResponse.isSuccessfullyCompleted = true;
            apiResponse.errorMessage = "";
            return apiResponse;
        }

        public ApiResponse<Users> GetUser(string username)
        {
            ApiResponse<Users> apiResponse = new();
            // var result = _context.Users.Find(username);
            var res = _context.Users.Find(username);
            if (res == null)
            {
                apiResponse.errorMessage = $"No Such User with Provided {nameof(username)} {username} is found in the table.";
                apiResponse.isSuccessfullyCompleted = false;
                apiResponse.Data = new();
                return apiResponse;
            }
            apiResponse.Data = res;
            apiResponse.isSuccessfullyCompleted = true;
            apiResponse.errorMessage = "";
            return apiResponse;
        }

        public ApiResponse<List<Users>> GetUsers()
        {
            var users = _context.Users.ToList();
            ApiResponse<List<Users>> apiResponse = new();
            apiResponse.Data = users;
            apiResponse.isSuccessfullyCompleted = true;
            return apiResponse;
        }

        public async Task<ApiResponse<Users>> UpdateUsersAsync(Users user)
        {
            ApiResponse<Users> apiResponse = new();
            var res = _context.Users.Find(user.UserName);
            if (res != null)
            {
                apiResponse.errorMessage = $"Sorry, No Such User with Provided {nameof(user.UserName)} {user.UserName} is Registered.";
                apiResponse.isSuccessfullyCompleted = false;
                apiResponse.Data = new();
                return apiResponse;
            }
            res.UserName = user.UserName;
            res.Password = user.Password;
            res.UserRole = user.UserRole;
            res.UpdatedOn = DateTime.Now;
            res.CreatedOn = res.CreatedOn;
            await _context.SaveChangesAsync();
            apiResponse.Data = user;
            apiResponse.isSuccessfullyCompleted = true;
            apiResponse.errorMessage = "";
            return apiResponse;
        }
    }
}
