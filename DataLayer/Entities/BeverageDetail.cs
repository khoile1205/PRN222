using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class BeverageDetail : BaseEntity
    {
        public string BeverageId { get; set; }
        public string SizeId { get; set; }
        public decimal Price { get; set; }

        public Beverage Beverage { get; set; }
        public BeverageSize Size { get; set; }
    }
}
