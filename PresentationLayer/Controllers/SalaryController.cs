using BussinessLayer.DTOs.Salary;
using BussinessLayer.Helper;
using BussinessLayer.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
    public class SalaryController : Controller
    {
        private readonly ISalaryService _salaryService;
        private readonly IUserService _userService;

        public SalaryController(ISalaryService salaryService, IUserService userService)
        {
            _salaryService = salaryService;
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var allUsers = await _userService.GetAllUsers();
            return View(allUsers);
        }

        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> ViewSalary(string? staffId, int? month, int? year)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var currentUserId = User.FindFirst("sub")?.Value;

            if (userRole == "Staff")
            {
                staffId = currentUserId;
            }
            else if (userRole == "Admin" && string.IsNullOrEmpty(staffId))
            {
                staffId = currentUserId;
            }

            if (string.IsNullOrEmpty(staffId))
                return Unauthorized();

            int selectedMonth = month ?? DateTime.Today.Month;
            int selectedYear = year ?? DateTime.Today.Year;

            DateTime startOfMonth = new DateTime(selectedYear, selectedMonth, 1);
            DateTime endDate = TimeHelper.GetVietnamTime();

            SalarySummaryDTO salaryData = await _salaryService.GetSalaryForStaffAsync(staffId, startOfMonth, endDate);

            ViewBag.SelectedMonth = selectedMonth;
            ViewBag.SelectedYear = selectedYear;

            return View(salaryData);
        }
    }
}
