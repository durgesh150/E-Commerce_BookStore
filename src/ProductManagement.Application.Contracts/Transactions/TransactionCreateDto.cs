using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProductManagement.Transactions
{
    public class TransactionCreateDto
    {
        public Guid UserId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TotalAmount { get; set; }
        [Required]
        public string PaymentStatus { get; set; }
    }
}