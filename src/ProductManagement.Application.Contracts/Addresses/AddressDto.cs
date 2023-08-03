using ProductManagement;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ProductManagement.Addresses
{
    public class AddressDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string CIty { get; set; }
        public string State { get; set; }
        public long PostalCode { get; set; }
        public Country Country { get; set; }
        public Guid UserId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}