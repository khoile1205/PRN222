﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Repositories.Abstraction;

namespace BussinessLayer.Services
{
    public class RoleService : IRoleService
    {
        private readonly IGenericRepository<Role> _roleRepository;

        public RoleService(IGenericRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public Task<IEnumerable<Role>> GetAllRoles()
        {
            return _roleRepository.GetAllAsync();
        }

        public async Task<Role?> GetRoleById(string roleId)
        {
            return await _roleRepository.GetAsync(r => r.Id == roleId);
        }

    }
}
