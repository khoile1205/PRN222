using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class Beverage : BaseEntity
    {
        public string CategoryId { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        public BeverageCategory BeverageCategory { get; set; }
        public virtual ICollection<BeverageDetail> BeverageDetails { get; set; } = new List<BeverageDetail>();
    }
}
