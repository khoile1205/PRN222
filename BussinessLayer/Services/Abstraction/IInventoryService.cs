using DataLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Services.Abstraction
{
    public interface IInventoryService
    {
        Task<Inventory> CreateInventoryAsync(Inventory inventory);
        Task<Inventory> GetInventoryByIdAsync(string id);
        Task<IEnumerable<Inventory>> GetAllInventoriesAsync();
        Task UpdateInventoryAsync(Inventory inventory);
        Task DeleteInventoryAsync(string id);
    }
}