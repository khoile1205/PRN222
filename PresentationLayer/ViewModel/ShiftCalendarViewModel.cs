namespace PresentationLayer.ViewModel
{
    public class ShiftCalendarViewModel
    {
        public DateTime Date { get; set; }
        public List<ShiftRegistrationDetail> Registrations { get; set; } = new List<ShiftRegistrationDetail>();
    }

    public class ShiftRegistrationDetail
    {
        public required string StaffName { get; set; }
        public required string ShiftDescription { get; set; }
        public DateTime ShiftStartTime { get; set; }
        public DateTime ShiftEndTime { get; set; }
    }

}
