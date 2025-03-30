using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Repositories.Abstraction;
using BussinessLayer.Helper;

namespace BussinessLayer.Services
{
    public class ShiftService : IShiftService
    {
        private readonly IGenericRepository<Shift> _shiftRepository;

        public ShiftService(IGenericRepository<Shift> shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }

        public async Task<IEnumerable<Shift>> GetAllShiftsAsync()
        {
            return await _shiftRepository.GetAllAsync();
        }

        public async Task<Shift?> GetShiftByIdAsync(string id)
        {
            return await _shiftRepository.GetAsync(s => s.Id == id);
        }

        public async Task CreateAsync(Shift shift)
        {
            shift.CreatedAt = TimeHelper.GetVietnamTime();
            shift.UpdatedAt = TimeHelper.GetVietnamTime();
            await _shiftRepository.CreateAsync(shift);
        }

        public async Task UpdateAsync(Shift shift)
        {
            shift.UpdatedAt = TimeHelper.GetVietnamTime();
            await _shiftRepository.UpdateAsync(shift);
        }

        public async Task RemoveAsync(Shift shift)
        {
            await _shiftRepository.RemoveAsync(shift);
        }
    }
}

