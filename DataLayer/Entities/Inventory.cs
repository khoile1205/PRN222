namespace DataLayer.Entities
{
    public class Inventory : BaseEntity
    {
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }

        public InventoryCategory InventoryCategory { get; set; }
        public virtual ICollection<InventoryUpdateHistory> InventoryUpdateHistory { get; set; } = new List<InventoryUpdateHistory>();
    }
}
