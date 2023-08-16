using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ProductManagement.Carts
{
    public class CartDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
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