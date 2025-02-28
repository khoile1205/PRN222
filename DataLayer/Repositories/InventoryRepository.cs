using DataLayer.Entities;
using DataLayer.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Inventory inventory)
        {
            await _context.Inventories.AddAsync(inventory);
            await SaveAsync();
        }

        public async Task<IEnumerable<Inventory>> GetAllAsync(Expression<Func<Inventory, bool>>? filter = null, Func<IQueryable<Inventory>, IQueryable<Inventory>>? includes = null)
        {
            IQueryable<Inventory> query = _context.Inventories;

            if (includes != null)
            {
                query = includes(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<Inventory> GetAsync(Expression<Func<Inventory, bool>> filter, Func<IQueryable<Inventory>, IQueryable<Inventory>>? includes = null)
        {
            IQueryable<Inventory> query = _context.Inventories;

            if (includes != null)
            {
                query = includes(query);
            }

            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task RemoveAsync(Inventory inventory)
        {
            _context.Inventories.Remove(inventory);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Inventory inventory)
        {
            _context.Inventories.Update(inventory);
            await SaveAsync();
        }
    }
}