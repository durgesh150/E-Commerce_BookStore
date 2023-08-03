using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ProductManagement.TransactionDetails
{
    public class TransactionDetailUpdateDto : IHasConcurrencyStamp
    {
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public Guid? TransactionId { get; set; }
        public Guid? BookId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}