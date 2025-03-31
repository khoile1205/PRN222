using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Enums;
using BussinessLayer.Helper;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using PresentationLayer.ViewModel;
using BussinessLayer.Authentication;
using System.Drawing.Printing;

namespace PresentationLayer.Controllers
{
    public class ShiftController : Controller
    {
        private readonly IShiftService _shiftService;
        private readonly IShiftStaffService _shiftStaffService;
        private readonly IUserService _userService;

        public ShiftController(IShiftService shiftService, IShiftStaffService shiftStaffService, IUserService userService)
        {
            _shiftService = shiftService;
            _shiftStaffService = shiftStaffService;
            _userService = userService;
        }

        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> RequestShift()
        {
            var shifts = await _shiftService.GetAllShiftsAsync();
            var shiftList = shifts.Select(s => new
            {
                Id = s.Id,
                Description = $"{s.Description} ({s.StartTime} - {s.EndTime})"
            });
            ViewData["ShiftId"] = new SelectList(shiftList, "Id", "Description");
            return View();
        }

        [Authorize(Roles = "Staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestShift([Bind("ShiftId,ShiftDate")] ShiftStaff shiftStaff)
        {
            var staffId = ClaimsPrincipalExtensions.GetUserId(User);
            if (string.IsNullOrEmpty(staffId))
            {
                Console.WriteLine("StaffId is missing in claims! Redirecting to login.");
                return RedirectToAction("Login", "Auth");
            }
            Console.WriteLine($"Retrieved StaffId: {staffId}");

            // Date validation
            if (shiftStaff.ShiftDate <= DateTime.Today)
            {
                TempData["ErrorMessage"] = "Shift date must be in the future.";
                return RedirectToAction("RequestShift");
            }

            try
            {
                var existingRequest = await _shiftStaffService.GetAllShiftRequestsAsync();
                if (existingRequest.Any(r => r.StaffId == staffId && r.ShiftDate.Date == shiftStaff.ShiftDate.Date && r.ShiftId == shiftStaff.ShiftId))
                {
                    TempData["ErrorMessage"] = "You already requested this shift on the selected date.";
                    return RedirectToAction("RequestShift");
                }

                shiftStaff.Id = Guid.NewGuid().ToString();
                shiftStaff.StaffId = staffId;
                shiftStaff.Status = RequestStatus.Pending;
                shiftStaff.CreatedAt = TimeHelper.GetVietnamTime();
                shiftStaff.UpdatedAt = TimeHelper.GetVietnamTime();

                await _shiftStaffService.RequestShiftAsync(shiftStaff);
                TempData["SuccessMessage"] = "Shift request submitted!";
                return RedirectToAction("MyRequests");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during request: {ex.Message}");
                TempData["ErrorMessage"] = "An error occurred while processing your request.";
                return RedirectToAction("RequestShift");
            }
        }

        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> MyRequests(int pageNumber = 1, int pageSize = 10, int? month = null, int? year = null)
        {
            var staffId = ClaimsPrincipalExtensions.GetUserId(User);
            if (string.IsNullOrEmpty(staffId))
            {
                Console.WriteLine("StaffId is missing in claims! Redirecting to login.");
                return RedirectToAction("Login", "Auth");
            }
            Console.WriteLine($"Retrieved StaffId: {staffId}");
            var result = await _shiftStaffService.GetShiftRequestsByStaffId(staffId, pageNumber, pageSize, month, year);
            return View(result);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveRequests()
        {
            var requests = await _shiftStaffService.GetAllShiftRequestsAsync();
            return View(requests.Where(r => r.Status == RequestStatus.Pending).ToList());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ApproveShift(string requestId)
        {
            await _shiftStaffService.UpdateShiftStatusAsync(requestId, RequestStatus.Accepted.ToString());
            TempData["SuccessMessage"] = "Shift approved.";
            return RedirectToAction("ApproveRequests");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> RejectShift(string requestId)
        {
            await _shiftStaffService.UpdateShiftStatusAsync(requestId, RequestStatus.Rejected.ToString());
            TempData["SuccessMessage"] = "Shift rejected.";
            return RedirectToAction("ApproveRequests");
        }

        //Add up

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ShiftCalendar()
        {
            // Load ALL approved shifts without range limit
            var approvedShifts = await _shiftStaffService.GetApprovedShiftRequestsByDateRangeAsync(DateTime.MinValue, DateTime.MaxValue);

            var groupedShifts = approvedShifts
                .GroupBy(s => s.ShiftDate.Date)
                .Select(g => new ShiftCalendarViewModel
                {
                    Date = g.Key,
                    Registrations = g.Select(x => new ShiftRegistrationDetail
                    {
                        StaffName = x.Staff.Name,
                        ShiftDescription = x.Shift.Description,
                        ShiftStartTime = x.ShiftDate.Date.Add(x.Shift.StartTime),
                        ShiftEndTime = x.ShiftDate.Date.Add(x.Shift.EndTime)
                    }).ToList()
                }).ToList();

            return View(groupedShifts);
        }
    }
}
