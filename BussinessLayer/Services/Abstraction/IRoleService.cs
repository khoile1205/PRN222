using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;

namespace BussinessLayer.Services.Abstraction
{
    public interface IRoleService
    {
        public Task<IEnumerable<Role>> GetAllRoles();
        Task<Role?> GetRoleById(string roleId);
    }
}
