using System;
using System.Collections.Generic;

namespace Caffeine.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string? Name { get; set; }

    public string? Position { get; set; }

    public decimal? Salary { get; set; }

    public string? Shift { get; set; }

    public string? Profile { get; set; }

    public string? Login { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
