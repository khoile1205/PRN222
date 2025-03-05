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
            var beverages = (await _beverageService.GetAllBeverages()).ToList()
                .Select(b => new BeverageViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    CategoryId = b.CategoryId,
                    CategoryName = b.BeverageCategory.CategoryName,
                    SizeId = b.BeverageDetails.Any() ? b.BeverageDetails.FirstOrDefault().SizeId : string.Empty,
                    Size = b.BeverageDetails.Any() && b.BeverageDetails.FirstOrDefault().Size != null
                            ? b.BeverageDetails.FirstOrDefault().Size.SizeName
                            : string.Empty,
                    Price = b.BeverageDetails.Any() ? b.BeverageDetails.FirstOrDefault().Price : 0,
                    ImageUrl = b.Image,
                    Description = b.Description,
                    CreatedAt = b.CreatedAt,
                    UpdatedAt = b.UpdatedAt,
                    DeletedAt = b.DeletedAt
                });

            return View(beverages);
        }

        // Create (GET)
        public async Task<IActionResult> Create()
        {

            ViewBag.Categories = (await _beverageCategoryService.GetAllBeverageCategories())
                                        .Select(c => new { c.Id, c.CategoryName })
                                        .ToList();

            ViewBag.Sizes = (await _beverageSizeService.GetAllBeverageSize())
                                    .Select(s => new { s.Id, Size = s.SizeName })
                                    .ToList();

            return View();
        }

        // Create (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBeverageDTO createBeverageDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Categories = (await _beverageCategoryService.GetAllBeverageCategories())
                                            .Select(c => new { c.Id, c.CategoryName })
                                            .ToList();

                    ViewBag.Sizes = (await _beverageSizeService.GetAllBeverageSize())
                                            .Select(s => new { Id = s.Id, Size = s.SizeName })
                                            .ToList();

                    return View(createBeverageDTO);
                }

                await _beverageService.CreateAsync(createBeverageDTO);

                TempData["SuccessMessage"] = "Beverage created successfully!";
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred while creating the beverage.";
            }

            // Reload ViewBag data in case of error
            ViewBag.Categories = (await _beverageCategoryService.GetAllBeverageCategories())
                                    .Select(c => new { c.Id, c.CategoryName })
                                    .ToList();

            ViewBag.Sizes = (await _beverageSizeService.GetAllBeverageSize())
                                    .Select(s => new { Id = s.Id, Size = s.SizeName })
                                    .ToList();

            return View(createBeverageDTO);
        }


        // Edit (GET)
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var beverage = await (from b in _context.Beverages
                                  join bd in _context.BeverageDetails on b.Id equals bd.BeverageId
                                  where b.Id == id
                                  select new BeverageViewModel
                                  {
                                      Id = b.Id,
                                      Name = b.Name,
                                      CategoryId = b.CategoryId,
                                      SizeId = bd.SizeId ?? string.Empty,
                                      Price = bd.Price,
                                      ImageUrl = b.Image,
                                      Description = b.Description,
                                      CreatedAt = b.CreatedAt,
                                      UpdatedAt = b.UpdatedAt,
                                      DeletedAt = b.DeletedAt
                                  }).FirstOrDefaultAsync();

            if (beverage == null) return NotFound();

            return View(beverage);
        }

        // Edit (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, BeverageViewModel viewModel)
        {
            try
            {
                var updateDto = new UpdateBeverageDTO
                {
                    Id = id,
                    Name = viewModel.Name,
                    ImageUrl = viewModel.ImageUrl ?? string.Empty,
                    Description = viewModel.Description,
                    SizeId = viewModel.SizeId,
                    Price = viewModel.Price
                };

                await _beverageService.UpdateBeverage(updateDto);
                TempData["SuccessMessage"] = "Beverage updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

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
    }
}
