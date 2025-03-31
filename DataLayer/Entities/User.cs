using DataLayer.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace DataLayer.Entities
{
    public class User : BaseEntity
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        [RegularExpression(@"^\S+$", ErrorMessage = "Username cannot contain spaces.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\W).{8,}$",
        ErrorMessage = "Password must be at least 8 characters long, contain at least 1 uppercase letter and 1 special character.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Phone number must start with 0 and be exactly 10 digits.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
        public string? Avatar { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }
        [DisplayName("Date of birth")]
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Position { get; set; }
        [DisplayName("Role")]
        public string RoleId { get; set; }
        public DateTime StartDate { get; set; }

        public Role Role { get; set; }
        public virtual ICollection<ShiftStaff> ShiftStaff { get; set; } = new List<ShiftStaff>();

    }
}
