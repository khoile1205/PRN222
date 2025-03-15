using BussinessLayer.Services;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Enums;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
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

        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            return View(transactions);
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

			ViewBag.Tables = await _tableService.GetAllTablesAsync();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(string TableId, string? VoucherCode, decimal finalPrice, List<TableBeverage> Items)
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
				PaymentType = PaymentType.Cashing
			};

			await _transactionService.CreateTransactionAsync(transaction);

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

			ViewBag.Tables = await _tableService.GetAllTablesAsync();
		}


		[HttpGet]
		public async Task<IActionResult> Check(string code)
		{
			var voucher = await _voucherService.GetVoucherByCodeAsync(code);

			if (voucher == null || voucher.EndDate < DateTime.Today)
			{
				return NotFound();
			}

			return Json(new
			{
				isValid = true,
				percentage = voucher.Percentage,
				maxDiscountAmount = voucher.MaxDiscountAmount
			});
		}



		public async Task<IActionResult> Details(string id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null) return NotFound();
            return View(transaction);
        }
    }
}
