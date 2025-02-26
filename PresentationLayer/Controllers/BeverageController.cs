using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    public class BeverageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BeverageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Index
        public async Task<IActionResult> Index()
        {
            var beverages = await _context.Beverages
                .Include(b => b.BeverageDetails)
                    .ThenInclude(bd => bd.Size)
                .Include(b => b.BeverageCategory)
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
                }).ToListAsync();

            return View(beverages);
        }

        // Create (GET)
        public IActionResult Create()
        {
            ViewBag.Categories = _context.BeverageCategories
                                         .Select(c => new { Id = c.Id, CategoryName = c.CategoryName })
                                         .ToList();

            ViewBag.Sizes = _context.BeverageSizes
                                    .Select(s => new { Id = s.Id, Size = s.SizeName })
                                    .ToList();

            return View();
        }

        // Create (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BeverageViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.BeverageCategories
                                             .Select(c => new { Id = c.Id, CategoryName = c.CategoryName })
                                             .ToList();

                ViewBag.Sizes = _context.BeverageSizes
                                        .Select(s => new { Id = s.Id, Size = s.SizeName })
                                        .ToList();

                return View(viewModel);
            }

            var beverageId = string.IsNullOrEmpty(viewModel.Id) ? Guid.NewGuid().ToString() : viewModel.Id;

            var beverage = new DataLayer.Entities.Beverage
            {
                Id = beverageId,
                Name = viewModel.Name,
                CategoryId = viewModel.CategoryId,
                Image = viewModel.ImageUrl,
                Description = viewModel.Description,
                CreatedAt = DateTime.Now
            };

            _context.Beverages.Add(beverage);
            await _context.SaveChangesAsync();

            var beverageDetail = new BeverageDetail
            {
                Id = Guid.NewGuid().ToString(),
                BeverageId = beverageId,
                SizeId = viewModel.SizeId,
                Price = viewModel.Price,
                CreatedAt = DateTime.Now
            };

            _context.BeverageDetails.Add(beverageDetail);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
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
            Console.WriteLine($"Editing Beverage ID: {id}");

            if (id != viewModel.Id)
            {
                Console.WriteLine("ID Mismatch");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ModelState.Remove("CategoryId");
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                foreach (var error in errors)
                {
                    Console.WriteLine($"Validation Error: {error}");
                }
                return View(viewModel);
            }

            var beverage = await _context.Beverages.FindAsync(id);
            if (beverage == null)
            {
                Console.WriteLine("Beverage not found");
                return NotFound();
            }

            beverage.Name = viewModel.Name;
            beverage.Image = viewModel.ImageUrl;
            beverage.Description = viewModel.Description;
            beverage.UpdatedAt = DateTime.Now;

            _context.Beverages.Update(beverage);

            var beverageDetail = await _context.BeverageDetails.FirstOrDefaultAsync(bd => bd.BeverageId == id);
            if (beverageDetail == null)
            {
                Console.WriteLine("BeverageDetail not found, creating a new one...");
                beverageDetail = new BeverageDetail
                {
                    Id = Guid.NewGuid().ToString(),
                    BeverageId = id,
                    SizeId = viewModel.SizeId ?? string.Empty,
                    Price = viewModel.Price,
                    CreatedAt = DateTime.Now
                };

                _context.BeverageDetails.Add(beverageDetail);
            }
            else
            {
                beverageDetail.SizeId = viewModel.SizeId ?? string.Empty;
                beverageDetail.Price = viewModel.Price;
                beverageDetail.UpdatedAt = DateTime.Now;

                _context.BeverageDetails.Update(beverageDetail);
            }

            await _context.SaveChangesAsync();

            Console.WriteLine("Update successful");
            return RedirectToAction(nameof(Index));
        }

        // Delete (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var beverageDetails = _context.BeverageDetails.Where(bd => bd.BeverageId == id);
            _context.BeverageDetails.RemoveRange(beverageDetails);

            var beverage = await _context.Beverages.FindAsync(id);
            if (beverage != null)
            {
                _context.Beverages.Remove(beverage);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BeverageExists(string id)
        {
            return _context.Beverages.Any(b => b.Id == id);
        }
    }
}
