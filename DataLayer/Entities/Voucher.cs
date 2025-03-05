using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class Voucher : BaseEntity
    {
        public string Code { get; set; }
        [Range(0, 100, ErrorMessage = "Percentage must be between 0 and 100")]
        public decimal Percentage { get; set; }
        public string Description { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Max discount amount must be a positive value.")]
        public decimal MaxDiscountAmount { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive value.")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End date is required and must be later than start date")]
        public DateTime EndDate { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
