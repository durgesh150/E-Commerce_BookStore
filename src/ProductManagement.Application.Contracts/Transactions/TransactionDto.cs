using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ProductManagement.Transactions
{
    public class TransactionDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public Guid UserId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}