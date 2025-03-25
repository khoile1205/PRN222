using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.Salary
{
    public class SalarySummaryDTO
    {
        public string StaffId { get; set; }
        public string StaffName { get; set; }
        public int TotalFullTimeShifts { get; set; }
        public int TotalPartTimeShifts { get; set; }
        public int TotalFullTimeHours { get; set; }
        public int TotalPartTimeHours { get; set; }
        public int TotalHours { get; set; }
        public decimal TotalSalary { get; set; }
    }
}
