using DataLayer.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace DataLayer.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Position { get; set; }
        public string RoleId { get; set; }
        public DateTime StartDate { get; set; }

        public Role Role { get; set; }
        public virtual ICollection<ShiftStaff> ShiftStaff { get; set; } = new List<ShiftStaff>();

    }
}
