using BussinessLayer.DTOs.Beverages;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{

    [Authorize]
    public class BeverageController : Controller
    {
        private readonly IBeverageService _beverageService;
        private readonly IBeverageCategoryService _beverageCategoryService;
        private readonly IBeverageSizeService _beverageSizeService;
        private readonly ApplicationDbContext _context;

        public BeverageController(ApplicationDbContext context, IBeverageService beverageService, IBeverageCategoryService beverageCategoryService, IBeverageSizeService beverageSizeService)
        {
            _context = context;
            _beverageService = beverageService;
            _beverageCategoryService = beverageCategoryService;
            _beverageSizeService = beverageSizeService;
        }

        // Index
        public async Task<IActionResult> Index()
        {
            var listBeverages = await _beverageService.GetAllBeverages();
            var beverages = listBeverages
                .Select(b => new BeverageViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    CategoryId = b.CategoryId,
                    CategoryName = b.BeverageCategory?.CategoryName ?? "Unknown",
                    Description = b.Description,
                    ImageUrl = b.Image,
                    CreatedAt = b.CreatedAt,
                    UpdatedAt = b.UpdatedAt,
                    DeletedAt = b.DeletedAt,
                    Details = b.BeverageDetails.Select(bd => new BeverageDetailViewModel
                    {
                        SizeId = bd.SizeId,
                        SizeName = bd.Size?.SizeName ?? "Unknown",
                        Price = bd.Price
                    }).ToList()
                });

            return View(beverages);
        }

        // Create (GET)
        public async Task<IActionResult> Create()
        {
            await PopulateViewBag();
            var model = new BeverageViewModel
            {
                Id = Guid.NewGuid().ToString(),
                Details = []
            };
            return View(model);
        }

        // Create (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BeverageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateViewBag();
                return View(model);
            }

            try
            {
                var createBeverageDTO = new CreateBeverageDTO
                {
                    Name = model.Name,
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,
                    Details = model.Details.Select(d => new BeverageDetailDTO
                    {
                        SizeId = d.SizeId,
                        Price = d.Price
                    }).ToList()
                };

                await _beverageService.CreateAsync(createBeverageDTO);
                TempData["SuccessMessage"] = "Beverage created successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                await PopulateViewBag();
                return View(model);
            }
        }


        // Edit (GET)
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var beverageEntity = await _beverageService.GetBeverageById(id);
            if (beverageEntity == null) return NotFound();

            var beverage = new BeverageViewModel
            {
                Id = beverageEntity.Id,
                Name = beverageEntity.Name,
                CategoryId = beverageEntity.CategoryId,
                CategoryName = beverageEntity.BeverageCategory?.CategoryName ?? "Unknown",
                Description = beverageEntity.Description,
                ImageUrl = beverageEntity.Image,
                CreatedAt = beverageEntity.CreatedAt,
                UpdatedAt = beverageEntity.UpdatedAt,
                DeletedAt = beverageEntity.DeletedAt,
                Details = beverageEntity.BeverageDetails.Select(bd => new BeverageDetailViewModel
                {
                    SizeId = bd.SizeId,
                    SizeName = bd.Size?.SizeName ?? "Unknown",
                    Price = bd.Price
                }).ToList()
            };
            await PopulateViewBag();
            return View(beverage);
        }

        // Edit (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, BeverageViewModel viewModel)
        {
            if (string.IsNullOrEmpty(id) || id != viewModel.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                await PopulateViewBag();
                return View(viewModel);
            }

            try
            {
                var updateDto = new UpdateBeverageDTO
                {
                    Id = id,
                    Name = viewModel.Name,
                    CategoryId = viewModel.CategoryId,
                    Description = viewModel.Description,
                    ImageUrl = viewModel.ImageUrl ?? string.Empty,
                    Details = viewModel.Details.Select(d => new BeverageDetailDTO
                    {
                        SizeId = d.SizeId,
                        Price = d.Price
                    }).ToList()
                };

                await _beverageService.UpdateBeverage(updateDto);
                TempData["SuccessMessage"] = "Beverage updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            await PopulateViewBag();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            try
            {
                var result = await _beverageService.DeleteBeverage(id);
                if (!result)
                {
                    TempData["ErrorMessage"] = "Failed to delete the beverage.";
                }
                else
                {
                    TempData["SuccessMessage"] = "Beverage deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the beverage.";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateViewBag()
        {
            var listBeverageCategories = await _beverageCategoryService.GetAllBeverageCategories();
            ViewBag.Categories = listBeverageCategories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.CategoryName
                }).ToList();

            var listBeverageSizes = await _beverageSizeService.GetAllBeverageSize();
            ViewBag.Sizes = listBeverageSizes
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.SizeName
                }).ToList();
        }
    }


}
