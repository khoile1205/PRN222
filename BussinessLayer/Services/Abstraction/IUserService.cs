using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Enums;

namespace BussinessLayer.Services.Abstraction
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetAllUsers();
        public Task<User> GetUserById(string id);
        public Task CreateUser(User user);
        public Task UpdateUser(User user);
        Task UpdateUserProfile(string userId, string name, string phoneNumber, Gender gender, string avatar);
        public Task<User?> GetUserByUserName(string userName);
    }
}
