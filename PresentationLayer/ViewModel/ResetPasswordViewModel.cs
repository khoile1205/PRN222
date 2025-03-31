using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.ViewModel
{
    public class ResetPasswordViewModel
    {
        public string StaffId { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\W).{8,}$",
    ErrorMessage = "Password must be at least 8 characters long, contain at least 1 uppercase letter and 1 special character.")]
        public string NewPassword { get; set; }
    }
}
