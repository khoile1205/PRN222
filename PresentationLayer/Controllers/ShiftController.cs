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

namespace PresentationLayer.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
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

        // View all shifts (for Admin or Staff)
        public async Task<IActionResult> Index()
        {
            var shifts = await _shiftService.GetAllShiftsAsync();
            return View(shifts);
        }

        // Staff requests shift
        public async Task<IActionResult> RequestShift()
        {
            var shifts = await _shiftService.GetAllShiftsAsync();
            ViewData["ShiftId"] = new SelectList(shifts, "Id", "Description");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestShift([Bind("ShiftId,ShiftDate")] ShiftStaff shiftStaff)
        {
            var staffId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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



        // View all my requests (Staff)
        public async Task<IActionResult> MyRequests()
        {
            var staffId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(staffId))
            {
                Console.WriteLine("StaffId is missing in claims! Redirecting to login.");
                return RedirectToAction("Login", "Auth");
            }
            Console.WriteLine($"Retrieved StaffId: {staffId}");


            var requests = await _shiftStaffService.GetAllShiftRequestsAsync();
            var myRequests = requests.Where(r => r.StaffId == staffId).ToList();
            return View(myRequests);
        }

        // Approve/Reject shift requests (Admin)
        public async Task<IActionResult> ApproveRequests()
        {
            var requests = await _shiftStaffService.GetAllShiftRequestsAsync();
            return View(requests.Where(r => r.Status == RequestStatus.Pending).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> ApproveShift(string requestId)
        {
            await _shiftStaffService.UpdateShiftStatusAsync(requestId, RequestStatus.Accepted.ToString());
            TempData["SuccessMessage"] = "Shift approved.";
            return RedirectToAction("ApproveRequests");
        }

        [HttpPost]
        public async Task<IActionResult> RejectShift(string requestId)
        {
            await _shiftStaffService.UpdateShiftStatusAsync(requestId, RequestStatus.Rejected.ToString());
            TempData["SuccessMessage"] = "Shift rejected.";
            return RedirectToAction("ApproveRequests");
        }

        //Add up

        // Admin view for upcoming approved shifts in calendar form
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ShiftCalendar()
        {
            // Optional: load next 30 days or configurable range
            var startDate = TimeHelper.GetVietnamTime().Date;
            var endDate = startDate.AddDays(30);

            var approvedShifts = await _shiftStaffService.GetApprovedShiftRequestsByDateRangeAsync(startDate, endDate);

            // Group by date to easily pass data to calendar
            var groupedShifts = approvedShifts
                .GroupBy(s => s.ShiftDate.Date)
                .Select(g => new ShiftCalendarViewModel
                {
                    Date = g.Key,
                    Registrations = g.Select(x => new ShiftRegistrationDetail
                    {
                        StaffName = x.Staff.Name,
                        ShiftDescription = x.Shift.Description,
                        ShiftStartTime = x.Shift.StartTime,
                        ShiftEndTime = x.Shift.EndTime
                    }).ToList()
                }).ToList();

            return View(groupedShifts);
        }

    }
}
