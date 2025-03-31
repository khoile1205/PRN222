using DataLayer.Enums;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.ViewModel
{
    public class ProfileViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Phone number must start with 0 and be exactly 10 digits.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Gender is required.")]
        public Gender Gender { get; set; }
        public string? Avatar { get; set; }
    }

}
