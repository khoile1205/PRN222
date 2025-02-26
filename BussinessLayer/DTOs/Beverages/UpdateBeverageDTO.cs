using System.ComponentModel.DataAnnotations;

namespace BussinessLayer.DTOs.Beverages
{
    public class UpdateBeverageDTO
    {
        [Required(ErrorMessage = "ID is required.")]
        public string Id { get; set; } = string.Empty;
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = string.Empty;
        public string? SizeId { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }
    }
}