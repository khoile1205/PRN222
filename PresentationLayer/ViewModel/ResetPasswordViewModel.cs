using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.ViewModel
{
    public class ResetPasswordViewModel
    {
        public string StaffId { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password needs at least 8 characters")]
        public string NewPassword { get; set; }
    }
}
