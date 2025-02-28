using DataLayer.Entities;

namespace DataLayer.Repositories.Abstraction
{
    public interface IInventoryRepository : IGenericRepository<Inventory>
    {
        //Task<IEnumerable<Inventory>> GetLowStockItemsAsync(string unitThreshold);
    }
}