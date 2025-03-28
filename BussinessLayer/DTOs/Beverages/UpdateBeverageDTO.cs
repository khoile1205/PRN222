using System.ComponentModel.DataAnnotations;

namespace BussinessLayer.DTOs.Beverages
{
    public class UpdateBeverageDTO
    {
        [Required(ErrorMessage = "ID is required.")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public string CategoryId { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "At least one size and price is required.")]
        public List<BeverageDetailDTO> Details { get; set; } = new List<BeverageDetailDTO>();
    }
}