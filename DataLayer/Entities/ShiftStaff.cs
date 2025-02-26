using DataLayer.Enums;

namespace DataLayer.Entities
{
    public class ShiftStaff : BaseEntity
    {
        public string StaffId { get; set; } // Foreign Key to User
        public DateTime ShiftDate { get; set; }
        public string ShiftId { get; set; } // Foreign Key to Shift
        public RequestStatus Status { get; set; }

        public virtual User? Staff { get; set; }
        public virtual Shift? Shift { get; set; }
    }
}
