using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Salary : BaseEntity
    {
        public string StaffId { get; set; }
        public DateTime SalaryDate { get; set; }
        public int FullTimeShiftCount { get; set; }
        public int PartTimeShiftCount { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalSalary { get; set; }

        public virtual User? Staff { get; set; }
    }
}
