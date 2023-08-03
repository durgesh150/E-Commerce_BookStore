using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ProductManagement.Authors
{
    public class AuthorDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string? Bio { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}