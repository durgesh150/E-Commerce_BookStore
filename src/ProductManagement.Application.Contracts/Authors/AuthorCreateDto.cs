using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProductManagement.Authors
{
    public class AuthorCreateDto
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = AuthorConsts.FirstNameMinLength)]
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? Bio { get; set; }
    }
}