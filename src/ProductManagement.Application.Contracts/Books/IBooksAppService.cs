using ProductManagement.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ProductManagement.Shared;

namespace ProductManagement.Books
{
    public interface IBooksAppService : IApplicationService
    {
        Task<PagedResultDto<BookWithNavigationPropertiesDto>> GetListAsync(GetBooksInput input);

        Task<BookWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<BookDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetAuthorLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<BookDto> CreateAsync(BookCreateDto input);

        Task<BookDto> UpdateAsync(Guid id, BookUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(BookExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}