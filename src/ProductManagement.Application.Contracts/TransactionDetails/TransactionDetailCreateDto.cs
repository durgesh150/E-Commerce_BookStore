using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProductManagement.TransactionDetails
{
    public class TransactionDetailCreateDto
    {
        public int Quantity { get; set; } = 1;
        public decimal SubTotal { get; set; }
        public Guid? TransactionId { get; set; }
        public Guid? BookId { get; set; }
    }
}