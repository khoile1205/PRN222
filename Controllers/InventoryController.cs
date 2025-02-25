using Microsoft.AspNetCore.Mvc;
using Caffeine.Models;
using Caffeine.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Caffeine.Controllers
{
    public class InventoryController : Controller
    {
        private readonly CaffeineContext _dbContext;

        public InventoryController(CaffeineContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var inventory = await _dbContext.Inventories
                                            .Include(i => i.Beverage)
                                            .ToListAsync();
            return View(inventory);
        }

        public IActionResult AddBeverage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBeverage(Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                var existingInventory = await _dbContext.Inventories
                    .FirstOrDefaultAsync(i => i.BeverageId == inventory.BeverageId);

                if (existingInventory != null)
                {
                    existingInventory.Quantity += inventory.Quantity;
                }
                else
                {
                    _dbContext.Inventories.Add(inventory);
                }

                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventory);
        }

        public async Task<IActionResult> UpdateInventory(int id)
        {
            var inventory = await _dbContext.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateInventory(Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Update(inventory);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventory);
        }

        public async Task<IActionResult> DeleteInventory(int id)
        {
            var inventory = await _dbContext.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }

        [HttpPost, ActionName("DeleteInventory")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventory = await _dbContext.Inventories.FindAsync(id);
            if (inventory != null)
            {
                _dbContext.Inventories.Remove(inventory);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
