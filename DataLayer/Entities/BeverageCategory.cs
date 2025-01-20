namespace DataLayer.Entities
{
    public class BeverageCategory : BaseEntity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Beverage> Beverages { get; set; } = new List<Beverage>();

    }
}
