using AutoMapper;
using BussinessLayer.DTOs.Salary;
using BussinessLayer.DTOs.ShiftStaff;
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
    public class SalaryService : ISalaryService
    {
        private const decimal FULL_TIME_HOURLY_RATE = 25000m;
        private const decimal PART_TIME_HOURLY_RATE = 20000m;

        private readonly IGenericRepository<ShiftStaff> _shiftStaffRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;
        public SalaryService(IGenericRepository<ShiftStaff> shiftStaffRepository,
                             IGenericRepository<User> userRepository, IMapper mapper)
        {
            _shiftStaffRepository = shiftStaffRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<SalarySummaryDTO> GetSalaryForStaffAsync(string staffId, int month, int year)
        {
            try
            {
                DateTime startOfMonth = DateTimeHelper.GetStartOfMonth(month, year);
                DateTime endOfMonth = DateTimeHelper.GetEndOfMonth(month, year);

                var existingStaff = await _userRepository.GetAsync(u => u.Id == staffId);
                if (existingStaff == null)
                {
                    throw new Exception("Staff member not found");
                }

                var shiftStaffs = await _shiftStaffRepository.GetAllAsync(
                    filter: s => s.StaffId == staffId
                                && s.ShiftDate >= startOfMonth
                                && s.ShiftDate <= endOfMonth
                                && s.Status == RequestStatus.Accepted,
                    includes: q => q.Include(s => s.Shift)
                                    .Include(s => s.Staff).ThenInclude(s => s.Role)
                );

                int fullTimeShifts = 0;
                int partTimeShifts = 0;
                int fullTimeHours = 0;
                int partTimeHours = 0;

                foreach (var record in shiftStaffs)
                {
                    if (record.Shift == null)
                        continue;

                    int shiftHours = (int)(record.Shift.EndTime - record.Shift.StartTime).TotalHours;

                    if (record.Shift.ShiftType == ShiftType.FullTime)
                    {
                        fullTimeShifts++;
                        fullTimeHours += shiftHours;
                    }
                    else if (record.Shift.ShiftType == ShiftType.PartTime)
                    {
                        partTimeShifts++;
                        partTimeHours += shiftHours;
                    }
                }

                int totalHours = fullTimeHours + partTimeHours;
                decimal totalSalary = (fullTimeHours * FULL_TIME_HOURLY_RATE) + (partTimeHours * PART_TIME_HOURLY_RATE);

                var shiftStaffDtos = _mapper.Map<IEnumerable<ShiftStaffDTO>>(shiftStaffs.OrderBy(ss => ss.ShiftDate));

                return new SalarySummaryDTO
                {
                    StaffId = staffId,
                    StaffName = existingStaff.Name,
                    StaffRole = existingStaff.Role.RoleName,
                    TotalFullTimeShifts = fullTimeShifts,
                    TotalPartTimeShifts = partTimeShifts,
                    TotalFullTimeHours = fullTimeHours,
                    TotalPartTimeHours = partTimeHours,
                    TotalHours = totalHours,
                    TotalSalary = totalSalary,
                    ShiftStaffs = shiftStaffDtos
                };
            }
            catch (Exception e)
            {
                throw new Exception("Error while getting salary data", e);
            }

        }
    }
}
