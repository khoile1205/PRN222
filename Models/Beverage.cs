using System;
using System.Collections.Generic;

namespace Caffeine.Models;

public partial class Beverage
{
    public int BeverageId { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public int? Inventory { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
