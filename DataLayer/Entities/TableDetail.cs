namespace DataLayer.Entities
{
    public class TableDetail : BaseEntity
    {
        public string TableId { get; set; } // Foreign Key to Table
        public string CustomerName { get; set; }

        public Table Table { get; set; }
        public Transaction Transaction { get; set; }
        public ICollection<TableBeverage> TableBeverages { get; set; }
    }
}
