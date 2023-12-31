using ProductManagement;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ProductManagement.Addresses
{
    public class AddressUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        public string CIty { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public long PostalCode { get; set; }
        public Country Country { get; set; }
        public Guid UserId { get; set; }
        [Required]
        public string StreetAddress { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}