using System;
using System.Collections.Generic;

namespace Caffeine.Models;

public partial class Bill
{
    public int BillId { get; set; }

    public int? StaffId { get; set; }

    public int? TableId { get; set; }

    public DateTime? DateTime { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual Staff? Staff { get; set; }

    public virtual CafeTable? Table { get; set; }
}
