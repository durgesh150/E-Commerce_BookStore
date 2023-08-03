using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ProductManagement.Books
{
    public class BookUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = BookConsts.TitleMinLength)]
        public string Title { get; set; }
        [Required]
        public float Price { get; set; }
        public string? Description { get; set; }
        public Guid AuthorId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}