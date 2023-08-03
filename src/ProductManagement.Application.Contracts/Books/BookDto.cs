using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ProductManagement.Books
{
    public class BookDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Title { get; set; }
        public float Price { get; set; }
        public string? Description { get; set; }
        public Guid AuthorId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}