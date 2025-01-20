using DataLayer.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class Transaction : BaseEntity
    {
        public string TableDetailId { get; set; }
        public decimal Price { get; set; }
        public string VoucherId { get; set; }
        public PaymentType PaymentType { get; set; }

        public TableDetail TableDetail { get; set; }
        public Voucher Voucher { get; set; }
    }
}
