using DataLayer.Entities;
using DataLayer.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class InventoryCategoryRepository : IInventoryCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryCategoryRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateAsync(InventoryCategory entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));
                await _context.InventoryCategories.AddAsync(entity);
                await SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating inventory category: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<InventoryCategory>> GetAllAsync(Expression<Func<InventoryCategory, bool>>? filter = null, Func<IQueryable<InventoryCategory>, IQueryable<InventoryCategory>>? includes = null)
        {
            try
            {
                IQueryable<InventoryCategory> query = _context.InventoryCategories;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (includes != null)
                {
                    query = includes(query);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching inventory categories: {ex.Message}", ex);
            }
        }

        public async Task<InventoryCategory> GetAsync(Expression<Func<InventoryCategory, bool>> filter, Func<IQueryable<InventoryCategory>, IQueryable<InventoryCategory>>? includes = null)
        {
            try
            {
                IQueryable<InventoryCategory> query = _context.InventoryCategories;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (includes != null)
                {
                    query = includes(query);
                }

                return await query.FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Inventory category not found.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching inventory category: {ex.Message}", ex);
            }
        }

        public async Task RemoveAsync(InventoryCategory entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));
                _context.InventoryCategories.Remove(entity);
                await SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing inventory category: {ex.Message}", ex);
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving changes: {ex.Message}", ex);
            }
        }

        public async Task UpdateAsync(InventoryCategory entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));
                _context.Entry(entity).State = EntityState.Modified;
                await SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating inventory category: {ex.Message}", ex);
            }
        }
    }
}