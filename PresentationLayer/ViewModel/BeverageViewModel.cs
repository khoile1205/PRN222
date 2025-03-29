using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class BeverageViewModel
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Category is required.")]
        public string CategoryId { get; set; }
        [Display(Name = "Category")]
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Image is required")]
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Collection for multiple sizes and prices
        [Required(ErrorMessage = "At least one size and price is required.")]
        [MinLength(1, ErrorMessage = "At least one size and price is required.")]
        public List<BeverageDetailViewModel> Details { get; set; } = [];
    }
    public class BeverageDetailViewModel
    {
        [Required(ErrorMessage = "Size is required.")]
        public string SizeId { get; set; }

        [Display(Name = "Size")]
        public string? SizeName { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }
    }
}