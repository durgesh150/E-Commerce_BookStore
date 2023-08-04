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
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ProductManagement.Shared;

namespace ProductManagement.Addresses
{

    [Authorize(ProductManagementPermissions.Addresses.Default)]
    public class AddressesAppService : ApplicationService, IAddressesAppService
    {
        private readonly IDistributedCache<AddressExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IAddressRepository _addressRepository;
        private readonly AddressManager _addressManager;

        public AddressesAppService(IAddressRepository addressRepository, AddressManager addressManager, IDistributedCache<AddressExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
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

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(AddressExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _addressRepository.GetListAsync(input.FilterText, input.CIty, input.State, input.PostalCodeMin, input.PostalCodeMax, input.Country, input.UserId, input.StreetAddress);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Address>, List<AddressExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Addresses.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new AddressExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}