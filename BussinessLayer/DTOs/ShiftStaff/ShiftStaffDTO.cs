using DataLayer.Enums;

namespace BussinessLayer.DTOs.ShiftStaff
{
    public class ShiftStaffDTO
    {
        public ShiftCode ShiftCode { get; set; }
        public string ShiftDescription { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public ShiftType ShiftType { get; set; }
        public DateTime ShiftDate { get; set; }
    }
}