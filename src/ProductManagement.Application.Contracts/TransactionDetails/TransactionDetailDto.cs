using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ProductManagement.TransactionDetails
{
    public class TransactionDetailDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public Guid? TransactionId { get; set; }
        public Guid? BookId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}