using ProductManagement.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProductManagement.TransactionDetails
{
    public interface ITransactionDetailsAppService : IApplicationService
    {
        Task<PagedResultDto<TransactionDetailWithNavigationPropertiesDto>> GetListAsync(GetTransactionDetailsInput input);

        Task<TransactionDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<TransactionDetailDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetTransactionLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetBookLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<TransactionDetailDto> CreateAsync(TransactionDetailCreateDto input);

        Task<TransactionDetailDto> UpdateAsync(Guid id, TransactionDetailUpdateDto input);
    }
}