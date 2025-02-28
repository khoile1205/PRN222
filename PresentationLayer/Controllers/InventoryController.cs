using Microsoft.AspNetCore.Mvc;
using DataLayer.Entities;
using BussinessLayer.Services.Abstraction;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PresentationLayer.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;
        private readonly IInventoryCategoryService _categoryService;

        public InventoryController(IInventoryService inventoryService, IInventoryCategoryService categoryService)
        {
            _inventoryService = inventoryService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var inventories = await _inventoryService.GetAllInventoriesAsync();
            return View(inventories);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllInventoryCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Inventory inventory)
        {
            var categories = await _categoryService.GetAllInventoryCategoriesAsync();
            if (!categories.Any())
            {
                ModelState.AddModelError("", "No categories available. Please create a category first.");
            }
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");

            if (string.IsNullOrEmpty(inventory.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Please select a valid category.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _inventoryService.CreateInventoryAsync(inventory);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error creating inventory: {ex.Message}");
                }
            }

            return View(inventory);
        }

        public async Task<IActionResult> Details(string id)
        {
            var inventory = await _inventoryService.GetInventoryByIdAsync(id);
            if (inventory == null) return NotFound();
            return View(inventory);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var inventory = await _inventoryService.GetInventoryByIdAsync(id);
            if (inventory == null) return NotFound();

            var categories = await _categoryService.GetAllInventoryCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName", inventory.CategoryId);
            return View(inventory);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Inventory inventory)
        {
            var categories = await _categoryService.GetAllInventoryCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName", inventory.CategoryId);

            // Kiểm tra validation từ ModelState
            if (!ModelState.IsValid)
            {
                return View(inventory); // Trả về view với lỗi validation từ annotation
            }

            try
            {
                // Cố gắng cập nhật inventory
                await _inventoryService.UpdateInventoryAsync(inventory);

                // Kiểm tra xem dữ liệu đã được lưu thành công chưa
                var updatedInventory = await _inventoryService.GetInventoryByIdAsync(inventory.Id);
                if (updatedInventory != null && updatedInventory.UpdatedAt == inventory.UpdatedAt)
                {
                    // Nếu dữ liệu được lưu thành công, quay về Index
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Nếu không tìm thấy hoặc dữ liệu không khớp, thêm lỗi
                    ModelState.AddModelError("", "Failed to save the inventory. Please try again.");
                }
            }
            catch (Exception ex)
            {
                // Nếu có exception từ service/repository, hiển thị lỗi
                ModelState.AddModelError("", $"Error updating inventory: {ex.Message}");
            }

            // Trả về view với lỗi nếu không lưu được
            return View(inventory);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var inventory = await _inventoryService.GetInventoryByIdAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var inventory = await _inventoryService.GetInventoryByIdAsync(id);
                if (inventory == null)
                {
                    ModelState.AddModelError("", "Inventory not found. It may have been deleted already.");
                    return View("Delete", new Inventory { Id = id }); // Trả về view với lỗi
                }

                await _inventoryService.DeleteInventoryAsync(id);

                // Kiểm tra xem inventory đã bị xóa chưa
                var deletedInventory = await _inventoryService.GetInventoryByIdAsync(id);
                if (deletedInventory == null || deletedInventory.DeletedAt != null) // Giả sử soft delete
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Failed to delete the inventory. Please try again.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error deleting inventory: {ex.Message}");
            }

            // Nếu có lỗi, trả về view Delete với dữ liệu hiện tại
            var inventoryToShow = await _inventoryService.GetInventoryByIdAsync(id) ?? new Inventory { Id = id };
            return View("Delete", inventoryToShow);
        }
    }
}