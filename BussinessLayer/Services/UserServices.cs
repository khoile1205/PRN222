using DataLayer.Entities;
using DataLayer.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetAllUsers();
        public Task<User> GetUserById(string id);
        public Task CreateUser(User user);
        public Task UpdateUser(User user);
        public Task DeleteUser(string id);
        public Task SaveUser();
    }

    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;

        public UserService(IGenericRepository<User> userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task CreateUser(User user)
        {
            await _userRepository.CreateAsync(user);
        }

        public Task DeleteUser(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllAsync(includeProperties: "Role");
        }

        public async Task<User> GetUserById(string id)
        {
            return await _userRepository.GetAsync(
            u => u.Id == id
            , includeProperties: "Role");
        }

        public async Task SaveUser()
        {
            await _userRepository.SaveAsync();
        }

        public async Task UpdateUser(User user)
        {
            await _userRepository.UpdateAsync(user);
        }
    }
}