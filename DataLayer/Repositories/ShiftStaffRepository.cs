using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Repositories.Abstraction;

namespace DataLayer.Repositories
{
    public class ShiftStaffRepository : GenericRepository<ShiftStaff>, IShiftStaffRepository
    {
        private readonly ApplicationDbContext _context;

        public ShiftStaffRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateShiftStaffStatusAsync(int id, string status)
        {
            var request = await _context.ShiftStaff.FindAsync(id);
            if (request != null)
            {
                if (Enum.TryParse<DataLayer.Enums.RequestStatus>(status, out var parsedStatus))
                {
                    request.Status = parsedStatus;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException($"Invalid status value: {status}");
                }
            }
        }

    }
}
