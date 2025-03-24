using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;

namespace BussinessLayer.Services.Abstraction
{
    public interface IShiftStaffService
    {
        Task<IEnumerable<ShiftStaff>> GetAllShiftRequestsAsync();
        Task<ShiftStaff?> GetShiftRequestByIdAsync(string id);
        Task RequestShiftAsync(ShiftStaff shiftStaff);
        Task UpdateShiftStatusAsync(string requestId, string newStatus);
    }
}

