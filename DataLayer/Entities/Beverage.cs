namespace DataLayer.Entities
{
    public class Beverage : BaseEntity
    {
        public string CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public BeverageCategory BeverageCategory { get; set; } = null!;

        public virtual ICollection<BeverageDetail> BeverageDetails { get; set; } = new List<BeverageDetail>();
    }
}
