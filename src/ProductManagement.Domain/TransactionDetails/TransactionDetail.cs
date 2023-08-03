using ProductManagement.Transactions;
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

namespace ProductManagement.TransactionDetails
{
    public class TransactionDetail : FullAuditedAggregateRoot<Guid>
    {
        public virtual int Quantity { get; set; }

        public virtual decimal SubTotal { get; set; }
        public Guid? TransactionId { get; set; }
        public Guid? BookId { get; set; }

        public TransactionDetail()
        {

        }

        public TransactionDetail(Guid id, Guid? transactionId, Guid? bookId, int quantity, decimal subTotal)
        {

            Id = id;
            Quantity = quantity;
            SubTotal = subTotal;
            TransactionId = transactionId;
            BookId = bookId;
        }

    }
}