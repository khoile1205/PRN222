using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Inventory : BaseEntity
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public string CategoryId { get; set; }

        [Required(ErrorMessage = "Unit is required.")]
        [StringLength(50, ErrorMessage = "Unit cannot exceed 50 characters.")]
        public string Unit { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        public string Quantity { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        public InventoryCategory InventoryCategory { get; set; }
        public virtual ICollection<InventoryUpdateHistory> InventoryUpdateHistory { get; set; } = new List<InventoryUpdateHistory>();
    }
}