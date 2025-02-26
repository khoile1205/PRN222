using BussinessLayer.Authentication;
using BussinessLayer.DTOs.Authentication;
using BussinessLayer.Helper;
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
    public class AuthService : IAuthService
    {
        private readonly IJwtService _jwtService;
        private readonly IGenericRepository<User> _userRepository;

        public AuthService(IJwtService jwtService, IGenericRepository<User> userRepository)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                User existingUser = await _userRepository.GetAsync(u => u.UserName == loginRequestDTO.Username || u.Email == loginRequestDTO.Username, includes: (q) => q.Include(u => u.Role));

                if (existingUser == null || !HashPasswordHelper.VerifyPassword(loginRequestDTO.Password, existingUser.Password))
                {
                    throw new Exception("Username or password is incorrect");
                }

                var token = _jwtService.GenerateToken(existingUser.Id, existingUser.UserName, existingUser.Role.RoleName);

                return new LoginResponseDTO
                {
                    AccessToken = token
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
