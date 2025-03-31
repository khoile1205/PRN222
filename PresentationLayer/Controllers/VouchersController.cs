using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer.Entities;
using BussinessLayer.Services.Abstraction;
using BussinessLayer.Services;
using BussinessLayer.Helper;
using Microsoft.AspNetCore.Authorization;

namespace PresentationLayer.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VouchersController : Controller
    {
        private readonly IVoucherService _voucherService;

        public VouchersController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        // GET: Vouchers/Index
        public async Task<IActionResult> Index()
        {
            var vouchers = await _voucherService.GetAllVouchersAsync();
            return View(vouchers);
        }

        // GET: Vouchers/Details/{id}
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var voucher = await _voucherService.GetVoucherByIdAsync(id);
            if (voucher == null)
            {
                return NotFound();
            }

            return View(voucher);
        }

        // GET: Vouchers/Create
        public IActionResult Create() 
        { 
            return View();
        }

        // POST: Vouchers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Percentage,Description,MaxDiscountAmount,Amount,StartDate,EndDate")] Voucher voucher)
        {
            ModelState.Remove("Code");
            ModelState.Remove("CreatedAt");
            ModelState.Remove("UpdatedAt");
            ModelState.Remove("DeletedAt");

            // Khởi tạo các giá trị tự động
            voucher.Id = Guid.NewGuid().ToString();
            voucher.DeletedAt = null;

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Failed to create voucher.";
                return View(voucher);
            }

            await _voucherService.CreateVoucherAsync(voucher);
            TempData["SuccessMessage"] = "Voucher created successfully!";
            return RedirectToAction("Index");
        }

        // GET: Vouchers/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voucher = await _voucherService.GetVoucherByIdAsync(id);
            if (voucher == null)
            {
                return NotFound();
            }
            return View(voucher);
        }

        // POST: Vouchers/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Percentage,Description,MaxDiscountAmount,Amount,StartDate,EndDate")] Voucher voucher)
        {
            voucher.Id = id;
            ModelState.Remove("Code");
            ModelState.Remove("CreatedAt");
            ModelState.Remove("UpdatedAt");
            ModelState.Remove("DeletedAt");

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                              .Select(e => e.ErrorMessage)
                              .ToList();
                Console.WriteLine(string.Join("\n", errors));
                return View(voucher);
            }

            try
            {
                voucher.UpdatedAt = TimeHelper.GetVietnamTime();
                await _voucherService.UpdateVoucherAsync(voucher);
                TempData["SuccessMessage"] = "Voucher updated successfully!";
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                TempData["ErrorMessage"] = "Data has been changed, please try again!";
                return View(voucher);
            }
        }

        // GET: Vouchers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var voucher = await _voucherService.GetVoucherByIdAsync(id);
            if(voucher == null)
                return NotFound();

            return View(voucher);
        }

        // POST: Vouchers/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _voucherService.DeleteVoucherAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
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
    }
}
