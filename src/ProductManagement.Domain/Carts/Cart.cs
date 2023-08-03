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
        public Guid? BookId { get; set; }

        public Cart()
        {

        }

        public Cart(Guid id, Guid? bookId, Guid userId, int quantity, DateTime dateAdded)
        {

            Id = id;
            UserId = userId;
            Quantity = quantity;
            DateAdded = dateAdded;
            BookId = bookId;
        }

    }
}