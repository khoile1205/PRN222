using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class BeverageSize : BaseEntity
    {
        public string Id { get; set; }
        public string SizeName { get; set; }
        public virtual ICollection<BeverageDetail> BeverageDetails { get; set; } = new List<BeverageDetail>();

    }
}
