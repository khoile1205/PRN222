using BussinessLayer.DTOs.Salary;
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
        private readonly IGenericRepository<Salary> _salaryRepository;
        private readonly IGenericRepository<User> _userRepository;

        public SalaryService(IGenericRepository<ShiftStaff> shiftStaffRepository,
                             IGenericRepository<Salary> salaryRepository,
                             IGenericRepository<User> userRepository)
        {
            _shiftStaffRepository = shiftStaffRepository;
            _salaryRepository = salaryRepository;
            _userRepository = userRepository;
        }

        public async Task<SalarySummaryDTO> GetSalaryForStaffAsync(string staffId, DateTime startDate, DateTime endDate)
        {
            var shiftStaffs = await _shiftStaffRepository.GetAllAsync(
                filter: s => s.StaffId.ToUpper() == staffId.ToUpper()
                            && s.ShiftDate >= startDate
                            && s.ShiftDate <= endDate,
                includes: q => q.Include(s => s.Shift)
                                .Include(s => s.Staff)
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

            var salaryRecord = new Salary
            {
                StaffId = staffId,
                SalaryDate = TimeHelper.GetVietnamTime(),
                FullTimeShiftCount = fullTimeShifts,
                PartTimeShiftCount = partTimeShifts,
                TotalHours = totalHours,
                TotalSalary = totalSalary
            };

            await _salaryRepository.CreateAsync(salaryRecord);

            string staffName;
            if (shiftStaffs.Any())
            {
                staffName = shiftStaffs.First().Staff?.Name;
            }
            else
            {
                var user = await _userRepository.GetAsync(u => u.Id.ToUpper() == staffId.ToUpper());
                staffName = user?.Name;
            }
            staffName = string.IsNullOrEmpty(staffName) ? "Unknown" : staffName;

            return new SalarySummaryDTO
            {
                StaffId = staffId,
                StaffName = staffName,
                TotalFullTimeShifts = fullTimeShifts,
                TotalPartTimeShifts = partTimeShifts,
                TotalFullTimeHours = fullTimeHours,
                TotalPartTimeHours = partTimeHours,
                TotalHours = totalHours,
                TotalSalary = totalSalary
            };
        }
    }
}
