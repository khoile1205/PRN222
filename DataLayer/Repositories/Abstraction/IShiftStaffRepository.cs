using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Abstraction
{
    public interface IShiftStaffRepository : IGenericRepository<ShiftStaff>
    {
        // Add 
        Task UpdateShiftStaffStatusAsync(int id, string status);
    }
}
