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
        public TimeSpan ShiftStartTime { get; set; }
        public TimeSpan ShiftEndTime { get; set; }
    }

}
