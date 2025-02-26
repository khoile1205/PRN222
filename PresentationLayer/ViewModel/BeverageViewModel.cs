using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class BeverageViewModel
    {
        [Required(ErrorMessage = "ID is required.")]
        public string Id { get; set; }

        public string? CategoryId { get; set; }

        public string? CategoryName { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public string SizeId { get; set; }

        public string? Size { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}