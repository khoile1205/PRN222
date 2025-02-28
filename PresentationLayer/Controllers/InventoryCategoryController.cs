using Microsoft.AspNetCore.Mvc;
using DataLayer.Entities;
using BussinessLayer.Services.Abstraction;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    public class InventoryCategoryController : Controller
    {
        private readonly IInventoryCategoryService _categoryService;

        public InventoryCategoryController(IInventoryCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // READ: List all categories
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllInventoryCategoriesAsync();
            return View(categories);
        }

        // CREATE: Show create form
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InventoryCategory category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    category.Id = Guid.NewGuid().ToString(); // Sinh Id mới
                    await _categoryService.CreateInventoryCategoryAsync(category);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error creating category: {ex.Message}");
                }
            }
            return View(category);
        }

        // READ: Show details
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var category = await _categoryService.GetInventoryCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // UPDATE: Show edit form
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var category = await _categoryService.GetInventoryCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InventoryCategory category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateInventoryCategoryAsync(category);
                    var updatedCategory = await _categoryService.GetInventoryCategoryByIdAsync(category.Id);
                    if (updatedCategory != null && updatedCategory.UpdatedAt == category.UpdatedAt)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to save the category. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating category: {ex.Message}");
                }
            }
            return View(category);
        }

        // DELETE: Show delete confirmation
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var category = await _categoryService.GetInventoryCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var category = await _categoryService.GetInventoryCategoryByIdAsync(id);
                if (category == null)
                {
                    ModelState.AddModelError("", "Category not found. It may have been deleted already.");
                    return View("Delete", new InventoryCategory { Id = id });
                }

                await _categoryService.DeleteInventoryCategoryAsync(id);

                var deletedCategory = await _categoryService.GetInventoryCategoryByIdAsync(id);
                if (deletedCategory == null || deletedCategory.DeletedAt != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Failed to delete the category. Please try again.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error deleting category: {ex.Message}");
            }

            var categoryToShow = await _categoryService.GetInventoryCategoryByIdAsync(id) ?? new InventoryCategory { Id = id };
            return View("Delete", categoryToShow);
        }
    }
}