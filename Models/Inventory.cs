using System;
using System.Collections.Generic;

namespace Caffeine.Models;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public int? BeverageId { get; set; }

    public int? Quantity { get; set; }

    public virtual Beverage? Beverage { get; set; }
}
