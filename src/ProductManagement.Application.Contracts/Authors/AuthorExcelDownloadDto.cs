using Volo.Abp.Application.Dtos;
using System;

namespace ProductManagement.Authors
{
    public class AuthorExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Bio { get; set; }

        public AuthorExcelDownloadDto()
        {

        }
    }
}