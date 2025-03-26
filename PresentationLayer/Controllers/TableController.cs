using BussinessLayer.Services;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PresentationLayer.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TableController : Controller
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }
        public async Task<IActionResult> Index()
        {
            var table = await _tableService.GetAllTablesAsync();
            return View(table);
        }

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _tableService.GetTableByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Create()
        {
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(TableStatus)));
            ViewBag.AreaList = new SelectList(Enum.GetValues(typeof(TableArea)));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Table table)
        {
            ModelState.Remove("TableDetails");

            if (ModelState.IsValid)
            {
                await _tableService.CreateTableAsync(table);
				TempData["SuccessMessage"] = "Table created successfully.";
				return RedirectToAction(nameof(Index));
            }

            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(TableStatus)));
            ViewBag.AreaList = new SelectList(Enum.GetValues(typeof(TableArea)));

            return View(table);
        }

        public async Task<IActionResult> Edit(string? id)
        {
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(TableStatus)));
            ViewBag.AreaList = new SelectList(Enum.GetValues(typeof(TableArea)));
            if (id == null)
            {
                return BadRequest("Table ID is required.");
            }

            var table = await _tableService.GetTableByIdAsync(id);
            if (table == null)
            {
                return NotFound("Table not found.");
            }

            return View(table);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Table table)
        {
            ModelState.Remove("TableDetails");

            if (ModelState.IsValid)
            {
                await _tableService.UpdateTableAsync(table);
				TempData["SuccessMessage"] = "Table updated successfully.";
				return RedirectToAction(nameof(Index));
            }
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(TableStatus)));
            ViewBag.AreaList = new SelectList(Enum.GetValues(typeof(TableArea)));
            return View(table);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return BadRequest("Table ID is required.");
            }

            var table = await _tableService.GetTableByIdAsync(id);
            if (table == null)
            {
                return NotFound("Table not found.");
            }

            return View(table);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var table = await _tableService.GetTableByIdAsync(id);
            if (table == null)
            {
                TempData["ErrorMessage"] = "Table not found.";
                return RedirectToAction(nameof(Index));
            }

            bool isDeleted = await _tableService.DeleteTableAsync(id);
            if (!isDeleted)
            {
                TempData["WarningMessage"] = "Cannot delete this table because it has been used. It has been marked as deleted.";
            }
            else
            {
                TempData["SuccessMessage"] = "Table deleted successfully.";
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(string id)
        {
            var table = await _tableService.GetTableByIdAsync(id);
            if (table == null)
            {
                TempData["ErrorMessage"] = "Table not found.";
                return RedirectToAction(nameof(Index));
            }

            bool isRestored = await _tableService.RestoreTableAsync(id);
            if (!isRestored)
            {
                TempData["ErrorMessage"] = "Failed to restore table.";
            }
            else
            {
                TempData["SuccessMessage"] = "Table restored successfully.";
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
