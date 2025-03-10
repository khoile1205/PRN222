using BussinessLayer.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Enums;

namespace PresentationLayer.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RevenueController : Controller
    {
        private readonly IRevenueService _revenueService;

        public RevenueController(IRevenueService revenueService)
        {
            _revenueService = revenueService;
        }

        public async Task<IActionResult> Index(RevenueRangeTypeEnum rangeType = RevenueRangeTypeEnum.Day, DateTime? startTime = null, DateTime? endTime = null)
        {
            var revenues = await _revenueService.GetRevenues(rangeType, startTime, endTime);

            ViewBag.RangeType = rangeType;
            ViewBag.StartTime = startTime;
            ViewBag.EndTime = endTime;

            return View(revenues);
        }
    }
}