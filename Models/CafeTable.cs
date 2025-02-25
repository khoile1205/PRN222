using System;
using System.Collections.Generic;

namespace Caffeine.Models;

public partial class CafeTable
{
    public int TableId { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
