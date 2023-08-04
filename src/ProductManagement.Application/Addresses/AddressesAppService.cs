using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using ProductManagement.Permissions;
using ProductManagement.Addresses;

namespace ProductManagement.Addresses
{

    [Authorize(ProductManagementPermissions.Addresses.Default)]
    public class AddressesAppService : ApplicationService, IAddressesAppService
    {

        private readonly IAddressRepository _addressRepository;
        private readonly AddressManager _addressManager;

        public AddressesAppService(IAddressRepository addressRepository, AddressManager addressManager)
        {

            _addressRepository = addressRepository;
            _addressManager = addressManager;
        }

        public virtual async Task<PagedResultDto<AddressDto>> GetListAsync(GetAddressesInput input)
        {
            var totalCount = await _addressRepository.GetCountAsync(input.FilterText, input.CIty, input.State, input.PostalCodeMin, input.PostalCodeMax, input.Country, input.UserId, input.StreetAddress);
            var items = await _addressRepository.GetListAsync(input.FilterText, input.CIty, input.State, input.PostalCodeMin, input.PostalCodeMax, input.Country, input.UserId, input.StreetAddress, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<AddressDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Address>, List<AddressDto>>(items)
            };
        }

        public virtual async Task<AddressDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Address, AddressDto>(await _addressRepository.GetAsync(id));
        }

        [Authorize(ProductManagementPermissions.Addresses.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _addressRepository.DeleteAsync(id);
        }

        [Authorize(ProductManagementPermissions.Addresses.Create)]
        public virtual async Task<AddressDto> CreateAsync(AddressCreateDto input)
        {

            var address = await _addressManager.CreateAsync(
            input.CIty, input.State, input.PostalCode, input.Country, input.UserId, input.StreetAddress
            );

            return ObjectMapper.Map<Address, AddressDto>(address);
        }

        [Authorize(ProductManagementPermissions.Addresses.Edit)]
        public virtual async Task<AddressDto> UpdateAsync(Guid id, AddressUpdateDto input)
        {

            var address = await _addressManager.UpdateAsync(
            id,
            input.CIty, input.State, input.PostalCode, input.Country, input.UserId, input.StreetAddress, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Address, AddressDto>(address);
        }
    }
}