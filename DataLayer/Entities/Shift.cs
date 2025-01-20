using DataLayer.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Shift : BaseEntity
    {
        public ShiftCode ShiftCode { get; set; }
        public string Description { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public ShiftType ShiftType {get; set;}
        public virtual ICollection<ShiftStaff> ShiftStaff { get; set; } = new List<ShiftStaff>();
    }
}
