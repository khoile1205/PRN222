using System.ComponentModel.DataAnnotations;

namespace BussinessLayer.DTOs.Beverages
{
    public class BeverageDetailDTO
    {
        [Required(ErrorMessage = "Size is required.")]
        public string SizeId { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

    }
}