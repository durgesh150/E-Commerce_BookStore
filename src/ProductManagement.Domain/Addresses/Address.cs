using ProductManagement;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ProductManagement.Addresses
{
    public class Address : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string CIty { get; set; }

        [NotNull]
        public virtual string State { get; set; }

        public virtual long PostalCode { get; set; }

        public virtual Country Country { get; set; }

        public virtual Guid UserId { get; set; }

        public Address()
        {

        }

        public Address(Guid id, string cIty, string state, long postalCode, Country country, Guid userId)
        {

            Id = id;
            Check.NotNull(cIty, nameof(cIty));
            Check.NotNull(state, nameof(state));
            if (postalCode < AddressConsts.PostalCodeMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(postalCode), postalCode, "The value of 'postalCode' cannot be lower than " + AddressConsts.PostalCodeMinLength);
            }

            if (postalCode > AddressConsts.PostalCodeMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(postalCode), postalCode, "The value of 'postalCode' cannot be greater than " + AddressConsts.PostalCodeMaxLength);
            }

            CIty = cIty;
            State = state;
            PostalCode = postalCode;
            Country = country;
            UserId = userId;
        }

    }
}