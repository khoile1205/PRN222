using BussinessLayer.Helper;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Enums;
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

        public async Task UpdateUserProfile(string userId, string name, string phoneNumber, Gender gender, string avatar)
        {
            var existingUser = await _userRepository.GetAsync(u => u.Id == userId);
            if (existingUser == null) throw new Exception("User not found");

            existingUser.Name = name;
            existingUser.PhoneNumber = phoneNumber;
            existingUser.Gender = gender;
            existingUser.Avatar = avatar;
            existingUser.UpdatedAt = TimeHelper.GetVietnamTime();

            await _userRepository.UpdateAsync(existingUser);
        }

    }
}