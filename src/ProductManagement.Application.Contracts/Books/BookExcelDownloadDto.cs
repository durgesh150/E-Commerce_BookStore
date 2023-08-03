using Volo.Abp.Application.Dtos;
using System;

namespace ProductManagement.Books
{
    public class BookExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? Title { get; set; }
        public float? PriceMin { get; set; }
        public float? PriceMax { get; set; }
        public string? Description { get; set; }
        public Guid? AuthorId { get; set; }

        public BookExcelDownloadDto()
        {

        }
    }
}