using BussinessLayer.Authentication;
using BussinessLayer.Services;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Enums;

namespace PresentationLayer.Controllers
{
	[Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IBeverageService _beverageService;
        private readonly ITableService _tableService;
		private readonly ITableDetailService _tableDetailService;
		private readonly IBeverageDetailService _beverageDetailService;
		private readonly IVoucherService _voucherService;

		public TransactionController(
            ITransactionService transactionService,
            IBeverageService beverageService,
            ITableService tableService,
			ITableDetailService tableDetailService,
			IBeverageDetailService beverageDetailService,
			IVoucherService voucherService
			)
        {
            _transactionService = transactionService;
            _beverageService = beverageService;
            _tableService = tableService;
			_tableDetailService = tableDetailService;
			_beverageDetailService = beverageDetailService;
			_voucherService = voucherService;
		}

        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate, int page = 1, int pageSize = 10)
        {
            var allTransactions = await _transactionService.GetAllTransactionsAsync(startDate, endDate, 0, int.MaxValue);
            int totalRecords = allTransactions.Count();

            var paginatedData = await _transactionService.GetAllTransactionsAsync(startDate, endDate, (page - 1) * pageSize, pageSize);

            ViewData["TotalPages"] = (int)Math.Ceiling((double)totalRecords / pageSize);
            ViewData["CurrentPage"] = page;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

            return View(paginatedData);
        }

        public async Task<IActionResult> Create()
        {
			var beverageDetails = await _beverageDetailService.GetAllBeverageDetailsAsync();
			ViewBag.BeverageDetails = beverageDetails;

			ViewBag.BeverageCategories = beverageDetails
				.Where(bd => bd.Beverage != null && bd.Beverage.BeverageCategory != null)
				.Select(bd => bd.Beverage.BeverageCategory)
				.Distinct()
				.OrderBy(c => c.CategoryName)
				.ToList();

			ViewBag.Tables = await _tableService.GetAvailableTablesAsync();
			ViewBag.PaymentTypes = new SelectList(
				Enum.GetValues(typeof(PaymentType))
					.Cast<PaymentType>()
					.Select(pt => new { Id = (int)pt, Name = pt.ToString() }),
				"Id", "Name"
			);


			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(string TableId, 
			string? VoucherCode, 
			decimal finalPrice, 
			List<TableBeverage> Items,
			PaymentType PaymentType)
		{
			if (Items == null || Items.Count == 0)
			{
				ModelState.AddModelError("", "Beverage is invalid!!");
				await ReloadViewData();
				return View();
			}


			Voucher? voucher = null;
			if (!string.IsNullOrEmpty(VoucherCode))
			{
				voucher = await _voucherService.GetVoucherByCodeAsync(VoucherCode);
				if (voucher == null)
				{
					ModelState.AddModelError("", "Invalid voucher.");
					await ReloadViewData();
					return View();
				}
			}

			var tableDetail = await _tableDetailService.GetTableDetaileByIdAsync(TableId);
			if (tableDetail == null)
			{
				tableDetail = new TableDetail
				{
					TableId = TableId,
					CustomerName = "Retail customer",
					TableBeverages = new List<TableBeverage>()
				};

				await _tableDetailService.CreateAsync(tableDetail);
				tableDetail = await _tableDetailService.GetTableDetaileByIdAsync(tableDetail.Id);

			}

			var beverageDetailIds = Items.Select(tb => tb.BeverageDetailId).ToList();
			var beverageDetails = (await _beverageDetailService.GetAllBeverageDetailsAsync())
									.Where(b => beverageDetailIds.Contains(b.Id))
									.ToDictionary(b => b.Id, b => b);

			foreach (var item in Items)
			{
				if (beverageDetails.TryGetValue(item.BeverageDetailId, out var beverageDetail))
				{
					item.BeverageDetail = beverageDetail;
					item.TableDetailId = tableDetail.Id;
				}
				else
				{
					ModelState.AddModelError("", $"Cannot find beverage with ID {item.BeverageDetailId}.");
					await ReloadViewData();
					return View();
				}
			}

			tableDetail.TableBeverages = Items;
			await _tableDetailService.UpdateAsync(tableDetail);

			var transaction = new Transaction
			{
				TableDetailId = tableDetail.Id,
				Price = finalPrice,
				VoucherId = voucher?.Id,
				PaymentType = PaymentType
			};

			await _transactionService.CreateTransactionAsync(transaction);
			TempData["SuccessMessage"] = "Order successfully!";


			return RedirectToAction("Index");
		}

		private async Task ReloadViewData()
		{
			var beverageDetails = await _beverageDetailService.GetAllBeverageDetailsAsync();
			ViewBag.BeverageDetails = beverageDetails;

			ViewBag.BeverageCategories = beverageDetails
				.Where(bd => bd.Beverage != null && bd.Beverage.BeverageCategory != null)
				.Select(bd => bd.Beverage.BeverageCategory)
				.Distinct()
				.OrderBy(c => c.CategoryName)
				.ToList();

			ViewBag.Tables = await _tableService.GetAvailableTablesAsync();
			ViewBag.PaymentTypes = new SelectList(
				Enum.GetValues(typeof(PaymentType))
				.Cast<PaymentType>()
				.Select(pt => new { Id = (int)pt, Name = pt.ToString() }),
				"Id", "Name"
	);
		}

		public async Task<IActionResult> Details(string id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null) return NotFound();
            return View(transaction);
        }
    }
}
