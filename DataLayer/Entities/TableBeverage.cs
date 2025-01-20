namespace DataLayer.Entities
{
    public class TableBeverage : BaseEntity
    {
        public string TableDetailId { get; set; }
        public string BeverageDetailId { get; set; }
        public int Quantity { get; set; }

        public TableDetail TableDetail { get; set; }
        public BeverageDetail BeverageDetail { get; set; }
    }
}
