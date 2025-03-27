using System.ComponentModel.DataAnnotations;
using DataLayer.Enums;

namespace DataLayer.Entities
{
    public class Table : BaseEntity
    {
        public string TableName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Seat quantity must be at least 1.")]
        public int SeatQuantity { get; set; }
        public TableArea Area { get; set; }

        public ICollection<TableDetail> TableDetails { get; set; }
    }
}
