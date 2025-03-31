using System.ComponentModel.DataAnnotations;
using DataLayer.Enums;

namespace DataLayer.Entities
{
    public class Table : BaseEntity
    {
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Table name must be between 3 and 50 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Table name can only contain letters, numbers, and spaces.")]
        public string TableName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Seat quantity must be at least 1.")]
        public int SeatQuantity { get; set; }
        public TableArea Area { get; set; }

        public ICollection<TableDetail> TableDetails { get; set; }
    }
}
