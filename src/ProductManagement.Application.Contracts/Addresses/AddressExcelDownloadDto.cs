using ProductManagement;
using Volo.Abp.Application.Dtos;
using System;

namespace ProductManagement.Addresses
{
    public class AddressExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? CIty { get; set; }
        public string? State { get; set; }
        public long? PostalCodeMin { get; set; }
        public long? PostalCodeMax { get; set; }
        public Country? Country { get; set; }
        public Guid? UserId { get; set; }
        public string? StreetAddress { get; set; }

        public AddressExcelDownloadDto()
        {

        }
    }
}