using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Repositories.Abstraction;

namespace DataLayer.Repositories
{
    public class ShiftRepository : GenericRepository<Shift>, IShiftRepository
    {
        public ShiftRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Add shift-specific methods here if needed
    }
}
