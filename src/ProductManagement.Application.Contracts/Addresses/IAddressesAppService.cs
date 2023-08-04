using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ProductManagement.Shared;

namespace ProductManagement.Addresses
{
    public interface IAddressesAppService : IApplicationService
    {
        Task<PagedResultDto<AddressDto>> GetListAsync(GetAddressesInput input);

        Task<AddressDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<AddressDto> CreateAsync(AddressCreateDto input);

        Task<AddressDto> UpdateAsync(Guid id, AddressUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(AddressExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}