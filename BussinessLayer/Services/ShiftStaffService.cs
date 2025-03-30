using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Repositories;
using DataLayer.Repositories.Abstraction;
using BussinessLayer.Helper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BussinessLayer.Services
{
    public class ShiftStaffService : IShiftStaffService
    {
        private readonly IGenericRepository<ShiftStaff> _shiftStaffRepository;
        private readonly IPaginationRepository<ShiftStaff> _shiftStaffPaginationRepository;

        public ShiftStaffService(IGenericRepository<ShiftStaff> shiftStaffRepository, IPaginationRepository<ShiftStaff> shiftStaffPaginationRepository)
        {
            _shiftStaffRepository = shiftStaffRepository;
            _shiftStaffPaginationRepository = shiftStaffPaginationRepository;
        }

        public async Task<IEnumerable<ShiftStaff>> GetAllShiftRequestsAsync()
        {
            return await _shiftStaffRepository.GetAllAsync(includes: ss => ss.Include(x => x.Shift).Include(x => x.Staff));
        }

        public async Task<PaginationResult<ShiftStaff>> GetShiftRequestsByStaffId(string staffId, int pageNumber = 1, int pageSize = 10, int? month = null, int? year = null)
        {
            var currentDate = DateTime.Now;
            month ??= currentDate.Month;
            year ??= currentDate.Year;

            Expression<Func<ShiftStaff, bool>> filter = r => r.StaffId == staffId
            && r.ShiftDate.Month == month.Value
            && r.ShiftDate.Year == year.Value;

            var listShiftStaff = await _shiftStaffPaginationRepository.GetPaginatedAsync(
                        pageNumber: pageNumber,
                        pageSize: pageSize,
                        filter: filter,
                    includes: q => q.Include(r => r.Staff).Include(r => r.Shift)
                    );
            listShiftStaff.Data = listShiftStaff.Data.OrderByDescending(r => r.ShiftDate).ToList();

            return listShiftStaff;
        }

        public async Task<ShiftStaff?> GetShiftRequestByIdAsync(string id)
        {
            return await _shiftStaffRepository.GetAsync(ss => ss.Id == id, includes: ss => ss.Include(x => x.Shift).Include(x => x.Staff));
        }

        public async Task RequestShiftAsync(ShiftStaff shiftStaff)
        {
            shiftStaff.CreatedAt = TimeHelper.GetVietnamTime();
            shiftStaff.UpdatedAt = TimeHelper.GetVietnamTime();
            await _shiftStaffRepository.CreateAsync(shiftStaff);
        }

        public async Task UpdateShiftStatusAsync(string requestId, string newStatus)
        {
            if (Enum.TryParse(newStatus, out DataLayer.Enums.RequestStatus parsedStatus))
            {
                var request = await _shiftStaffRepository.GetAsync(ss => ss.Id == requestId);
                if (request != null)
                {
                    request.Status = parsedStatus;
                    request.UpdatedAt = TimeHelper.GetVietnamTime();
                    await _shiftStaffRepository.UpdateAsync(request);
                }
            }
            else
            {
                throw new ArgumentException("Invalid shift status.");
            }
        }

        //Add up
        public async Task<List<ShiftStaff>> GetApprovedShiftRequestsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var results = await _shiftStaffRepository.GetAllAsync(
                filter: ss => ss.Status == DataLayer.Enums.RequestStatus.Accepted
                              && ss.ShiftDate.Date >= startDate.Date
                              && ss.ShiftDate.Date <= endDate.Date,
                includes: ss => ss.Include(x => x.Shift).Include(x => x.Staff)
            );

            return results.ToList();
        }

    }
}