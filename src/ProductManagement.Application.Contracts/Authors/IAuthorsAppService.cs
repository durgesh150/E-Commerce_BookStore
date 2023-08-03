using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ProductManagement.Shared;

namespace ProductManagement.Authors
{
    public interface IAuthorsAppService : IApplicationService
    {
        Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorsInput input);

        Task<AuthorDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<AuthorDto> CreateAsync(AuthorCreateDto input);

        Task<AuthorDto> UpdateAsync(Guid id, AuthorUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(AuthorExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}