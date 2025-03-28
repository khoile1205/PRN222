using System.ComponentModel.DataAnnotations;

namespace BussinessLayer.DTOs.Beverages
{
    public class CreateBeverageDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public string CategoryId { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        // Collection for multiple sizes and prices
        [Required(ErrorMessage = "At least one size and price is required.")]
        public List<BeverageDetailDTO> Details { get; set; } = new List<BeverageDetailDTO>();

    }
}