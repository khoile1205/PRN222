using System.Linq.Expressions;
using DataLayer.Entities;

namespace DataLayer.Repositories.Abstraction
{
    public interface IPaginationRepository<T> where T : class
    {
        Task<PaginationResult<T>> GetPaginatedAsync(int pageNumber = 1, int pageSize = 10, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IQueryable<T>>? includes = null);
    }
}