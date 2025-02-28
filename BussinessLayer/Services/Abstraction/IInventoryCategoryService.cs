using DataLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Services.Abstraction
{
    public interface IInventoryCategoryService
    {
        Task<InventoryCategory> CreateInventoryCategoryAsync(InventoryCategory category);
        Task<InventoryCategory> GetInventoryCategoryByIdAsync(string id);
        Task<IEnumerable<InventoryCategory>> GetAllInventoryCategoriesAsync();
        Task UpdateInventoryCategoryAsync(InventoryCategory category);
        Task DeleteInventoryCategoryAsync(string id);
    }
}