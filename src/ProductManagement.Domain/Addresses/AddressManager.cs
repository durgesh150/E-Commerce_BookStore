using ProductManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ProductManagement.Addresses
{
    public class AddressManager : DomainService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressManager(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<Address> CreateAsync(
        string cIty, string state, long postalCode, Country country, Guid userId, string streetAddress)
        {
            Check.NotNullOrWhiteSpace(cIty, nameof(cIty));
            Check.NotNullOrWhiteSpace(state, nameof(state));
            Check.Range(postalCode, nameof(postalCode), AddressConsts.PostalCodeMinLength, AddressConsts.PostalCodeMaxLength);
            Check.NotNull(country, nameof(country));
            Check.NotNullOrWhiteSpace(streetAddress, nameof(streetAddress));

            var address = new Address(
             GuidGenerator.Create(),
             cIty, state, postalCode, country, userId, streetAddress
             );

            return await _addressRepository.InsertAsync(address);
        }

        public async Task<Address> UpdateAsync(
            Guid id,
            string cIty, string state, long postalCode, Country country, Guid userId, string streetAddress, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(cIty, nameof(cIty));
            Check.NotNullOrWhiteSpace(state, nameof(state));
            Check.Range(postalCode, nameof(postalCode), AddressConsts.PostalCodeMinLength, AddressConsts.PostalCodeMaxLength);
            Check.NotNull(country, nameof(country));
            Check.NotNullOrWhiteSpace(streetAddress, nameof(streetAddress));

            var address = await _addressRepository.GetAsync(id);

            address.CIty = cIty;
            address.State = state;
            address.PostalCode = postalCode;
            address.Country = country;
            address.UserId = userId;
            address.StreetAddress = streetAddress;

            address.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _addressRepository.UpdateAsync(address);
        }

    }
}