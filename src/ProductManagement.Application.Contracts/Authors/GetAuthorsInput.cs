using Volo.Abp.Application.Dtos;
using System;

namespace ProductManagement.Authors
{
    public class GetAuthorsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Bio { get; set; }

        public GetAuthorsInput()
        {

        }
    }
}