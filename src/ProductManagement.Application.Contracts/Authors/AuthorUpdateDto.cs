using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ProductManagement.Authors
{
    public class AuthorUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = AuthorConsts.FirstNameMinLength)]
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? Bio { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}