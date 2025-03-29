using BussinessLayer.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using Shared.Enums;
using System.Text;

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

        [HttpGet]
        public async Task<IActionResult> ExportToExcel(RevenueRangeTypeEnum rangeType, DateTime? startTime, DateTime? endTime)
        {
            (startTime, endTime) = _revenueService.GetRevenueDateRange(rangeType, startTime, endTime);
            var revenues = await _revenueService.GetRevenues(rangeType, startTime, endTime);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Revenue Data");

                // Add headers
                worksheet.Cells[1, 1].Value = "Period";
                worksheet.Cells[1, 2].Value = "Revenue";

                // Add data
                int row = 2;
                foreach (var revenue in revenues)
                {
                    worksheet.Cells[row, 1].Value = revenue.Label;
                    worksheet.Cells[row, 2].Value = revenue.Revenue;
                    row++;
                }
                worksheet.Column(2).Style.Numberformat.Format = "$#,##0.00";

                String fileName = $"Revenue_{rangeType}_From_{startTime:yyyyMMdd}_To_{endTime:yyyyMMdd}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                // Auto-fit columns
                worksheet.Cells.AutoFitColumns();

                var stream = new MemoryStream(package.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ExportToCsv(RevenueRangeTypeEnum rangeType,
            DateTime? startTime, DateTime? endTime)
        {
            (startTime, endTime) = _revenueService.GetRevenueDateRange(rangeType, startTime, endTime);
            var revenues = await _revenueService.GetRevenues(rangeType, startTime, endTime);

            var csvContent = new StringBuilder();

            // Add headers
            csvContent.AppendLine("Period,Revenue");

            // Add data
            foreach (var revenue in revenues)
            {
                // Escape commas and quotes in the label if necessary
                string period = revenue.Label.Contains(",") || revenue.Label.Contains("\"")
                    ? $"\"{revenue.Label.Replace("\"", "\"\"")}\""
                    : revenue.Label;
                string formattedRevenue = revenue.Revenue.ToString("C");
                csvContent.AppendLine($"{period},{formattedRevenue}");
            }

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(csvContent.ToString()));
            string fileName = $"Revenue_{rangeType}_From_{startTime:yyyyMMdd}_To_{endTime:yyyyMMdd}_{DateTime.Now:yyyyMMddHHmmss}.csv";
            return File(stream, "text/csv", fileName);
        }
    }
}