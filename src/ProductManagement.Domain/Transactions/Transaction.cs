using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ProductManagement.Transactions
{
    public class Transaction : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid UserId { get; set; }

        public virtual DateTime TransactionDate { get; set; }

        public virtual decimal TotalAmount { get; set; }

        [NotNull]
        public virtual string PaymentStatus { get; set; }

        public Transaction()
        {

        }

        public Transaction(Guid id, Guid userId, DateTime transactionDate, decimal totalAmount, string paymentStatus)
        {

            Id = id;
            Check.NotNull(paymentStatus, nameof(paymentStatus));
            UserId = userId;
            TransactionDate = transactionDate;
            TotalAmount = totalAmount;
            PaymentStatus = paymentStatus;
        }

    }
}