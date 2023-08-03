using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ProductManagement.Transactions
{
    public class TransactionUpdateDto : IHasConcurrencyStamp
    {
        public Guid UserId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TotalAmount { get; set; }
        [Required]
        public string PaymentStatus { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}