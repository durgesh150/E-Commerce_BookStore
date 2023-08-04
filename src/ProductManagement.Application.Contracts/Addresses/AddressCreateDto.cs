using ProductManagement;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProductManagement.Addresses
{
    public class AddressCreateDto
    {
        [Required]
        public string CIty { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        [Range(AddressConsts.PostalCodeMinLength, AddressConsts.PostalCodeMaxLength)]
        public long PostalCode { get; set; }
        public Country Country { get; set; } = ((Country[])Enum.GetValues(typeof(Country)))[0];
        public Guid UserId { get; set; }
        [Required]
        public string StreetAddress { get; set; }
    }
}