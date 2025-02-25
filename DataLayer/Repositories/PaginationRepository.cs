using System.Linq.Expressions;
using DataLayer.Entities;
using DataLayer.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class PaginationRepository<T> : IPaginationRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        internal DbSet<T> _dbSet;

        public PaginationRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<PaginationResult<T>> GetPaginatedAsync(int pageNumber = 1, int pageSize = 10, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IQueryable<T>>? includes = null)
        {
            try
            {
                IQueryable<T> query = _dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }
                if (includes != null)
                {
                    query = includes(query);
                }

                var totalCount = await query.CountAsync();

                IEnumerable<T> totalItems = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

                return new PaginationResult<T>(totalItems, totalCount, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching all records: {ex.Message}", ex);
            }
        }
    }
}