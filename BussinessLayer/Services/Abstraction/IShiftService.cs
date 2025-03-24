using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;

namespace BussinessLayer.Services.Abstraction
{
    public interface IShiftService
    {
        Task<IEnumerable<Shift>> GetAllShiftsAsync();
        Task<Shift?> GetShiftByIdAsync(string id);
        Task CreateAsync(Shift shift);
        Task UpdateAsync(Shift shift);
        Task RemoveAsync(Shift shift);
    }
}

