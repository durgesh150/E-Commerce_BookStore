using ProductManagement;
using System;

namespace ProductManagement.Addresses
{
    public class AddressExcelDto
    {
        public string CIty { get; set; }
        public string State { get; set; }
        public long PostalCode { get; set; }
        public Country Country { get; set; }
        public Guid UserId { get; set; }
        public string StreetAddress { get; set; }
    }
}