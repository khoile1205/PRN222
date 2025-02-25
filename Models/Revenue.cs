using System;
using System.Collections.Generic;

namespace Caffeine.Models;

public partial class Revenue
{
    public int RevenueId { get; set; }

    public DateOnly? Date { get; set; }

    public decimal? TotalRevenue { get; set; }
}
