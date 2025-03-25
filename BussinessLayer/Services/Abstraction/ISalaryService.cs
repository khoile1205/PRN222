using BussinessLayer.DTOs.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services.Abstraction
{
    public interface ISalaryService
    {
        Task<SalarySummaryDTO> GetSalaryForStaffAsync(string staffId, DateTime startDate, DateTime endDate);
    }
}
