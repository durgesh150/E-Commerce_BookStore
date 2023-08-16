using ProductManagement.Books;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ProductManagement.Carts
{
    public class Cart : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid UserId { get; set; }

        public virtual int Quantity { get; set; }

        public virtual DateTime DateAdded { get; set; }

        public virtual decimal UnitPrice { get; set; }

        public virtual decimal TotalPrice { get; set; }

        public virtual DateTime? LastModified { get; set; }
        public Guid BookId { get; set; }

        public Cart()
        {

        }

        public Cart(Guid id, Guid bookId, Guid userId, int quantity, DateTime dateAdded, decimal unitPrice, decimal totalPrice, DateTime? lastModified = null)
        {

            Id = id;
            UserId = userId;
            Quantity = quantity;
            DateAdded = dateAdded;
            UnitPrice = unitPrice;
            TotalPrice = totalPrice;
            LastModified = lastModified;
            BookId = bookId;
        }

    }
}