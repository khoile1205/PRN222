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
        Task<PaginationResult<ShiftStaff>> GetShiftRequestsByStaffId(string staffId, int month, int year, int pageNumber = 1, int pageSize = 10);
        // Add up
        Task<List<ShiftStaff>> GetApprovedShiftRequestsByDateRangeAsync(DateTime startDate, DateTime endDate);

    }
}

