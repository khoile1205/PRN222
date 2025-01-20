using DataLayer.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class InventoryUpdateHistory : BaseEntity
    {
        public string InventoryId { get; set; }
        public decimal Spending { get; set; }
        public Units Unit { get; set; }
        public decimal Quantity { get; set; }

        // Navigation property
        public Inventory Inventory { get; set; }
    }
}
