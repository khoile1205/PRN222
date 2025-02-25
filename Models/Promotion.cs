using System;
using System.Collections.Generic;

namespace Caffeine.Models;

public partial class Promotion
{
    public int PromotionId { get; set; }

    public string? Description { get; set; }

    public decimal? Discount { get; set; }
}
