using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class Voucher : BaseEntity
    {
        public string Code { get; set; } // Unique
        public decimal Percentage { get; set; }
        public string Description { get; set; }
        public decimal MaxDiscountAmount { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
