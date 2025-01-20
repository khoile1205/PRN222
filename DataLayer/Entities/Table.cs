using DataLayer.Enums;

namespace DataLayer.Entities
{
    public class Table : BaseEntity
    {
        public string TableName { get; set; }
        public int SeatQuantity { get; set; }
        public TableStatus Status { get; set; }
        public TableArea Area { get; set; }

        public ICollection<TableDetail> TableDetails { get; set; }
    }
}
