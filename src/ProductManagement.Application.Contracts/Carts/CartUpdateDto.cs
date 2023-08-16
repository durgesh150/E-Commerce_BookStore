using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ProductManagement.Carts
{
    public class CartUpdateDto : IHasConcurrencyStamp
    {
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime? LastModified { get; set; }
        public Guid BookId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}