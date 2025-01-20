namespace DataLayer.Entities
{
    public class InventoryCategory : BaseEntity
    {
        public string CategoryName { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
    }
}
