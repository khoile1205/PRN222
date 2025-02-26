using BussinessLayer.Services.Abstraction;
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

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllAsync(includes: u => u.Include(u => u.Role));
        }

        public async Task<User> GetUserById(string id)
        {
            return await _userRepository.GetAsync(u => u.Id == id, includes: u => u.Include(u => u.Role));
        }

        public async Task UpdateUser(User user)
        {
            await _userRepository.UpdateAsync(user);
        }
    }
}