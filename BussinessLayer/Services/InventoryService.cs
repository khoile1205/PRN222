using DataLayer.Entities;
using DataLayer.Repositories.Abstraction;
using BussinessLayer.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<Inventory> CreateInventoryAsync(Inventory inventory)
        {
            try
            {
                inventory.UpdatedAt = DateTime.Now;
                await _inventoryRepository.CreateAsync(inventory);
                return inventory;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating inventory: {ex.Message}", ex);
            }
        }

        public async Task<Inventory> GetInventoryByIdAsync(string id)
        {
            try
            {
                return await _inventoryRepository.GetAsync(i => i.Id == id,
                    includes: q => q.Include(i => i.InventoryCategory)
                                   .Include(i => i.InventoryUpdateHistory));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching inventory: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Inventory>> GetAllInventoriesAsync()
        {
            try
            {
                return await _inventoryRepository.GetAllAsync(
                    includes: q => q.Include(i => i.InventoryCategory)
                                   .Include(i => i.InventoryUpdateHistory));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching inventories: {ex.Message}", ex);
            }
        }

        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            try
            {
                var existingInventory = await _inventoryRepository.GetAsync(i => i.Id == inventory.Id);
                if (existingInventory == null)
                    throw new Exception("Inventory not found");

                existingInventory.Name = inventory.Name;
                existingInventory.CategoryId = inventory.CategoryId;
                existingInventory.Unit = inventory.Unit;
                existingInventory.Quantity = inventory.Quantity;
                existingInventory.Description = inventory.Description;
                existingInventory.UpdatedAt = DateTime.UtcNow;

                await _inventoryRepository.UpdateAsync(existingInventory);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating inventory: {ex.Message}");
                throw new Exception($"Error updating inventory: {ex.Message}", ex);
            }
        }

        public async Task DeleteInventoryAsync(string id)
        {
            try
            {
                var inventory = await GetInventoryByIdAsync(id);
                if (inventory == null)
                    throw new Exception("Inventory not found");

                await _inventoryRepository.RemoveAsync(inventory);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting inventory: {ex.Message}");
                throw new Exception($"Error deleting inventory: {ex.Message}", ex);
            }
        }
    }
}