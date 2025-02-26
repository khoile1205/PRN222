using System.ComponentModel.DataAnnotations;

namespace BussinessLayer.DTOs.Beverages
{
    public class CreateBeverageDTO
    {
        public string? CategoryId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public string SizeId { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
    }
}