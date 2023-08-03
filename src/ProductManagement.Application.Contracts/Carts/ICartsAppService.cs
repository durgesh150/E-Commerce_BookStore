using ProductManagement.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProductManagement.Carts
{
    public interface ICartsAppService : IApplicationService
    {
        Task<PagedResultDto<CartWithNavigationPropertiesDto>> GetListAsync(GetCartsInput input);

        Task<CartWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<CartDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetBookLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<CartDto> CreateAsync(CartCreateDto input);

        Task<CartDto> UpdateAsync(Guid id, CartUpdateDto input);
        Task<List<CartDto>> GetByUserId(Guid? userid);
    }
}