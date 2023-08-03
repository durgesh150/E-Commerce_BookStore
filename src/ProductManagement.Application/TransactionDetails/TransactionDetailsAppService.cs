using ProductManagement.Shared;
using ProductManagement.Books;
using ProductManagement.Transactions;
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
using ProductManagement.TransactionDetails;

namespace ProductManagement.TransactionDetails
{

    [Authorize(ProductManagementPermissions.TransactionDetails.Default)]
    public class TransactionDetailsAppService : ApplicationService, ITransactionDetailsAppService
    {

        private readonly ITransactionDetailRepository _transactionDetailRepository;
        private readonly TransactionDetailManager _transactionDetailManager;
        private readonly IRepository<Transaction, Guid> _transactionRepository;
        private readonly IRepository<Book, Guid> _bookRepository;

        public TransactionDetailsAppService(ITransactionDetailRepository transactionDetailRepository, TransactionDetailManager transactionDetailManager, IRepository<Transaction, Guid> transactionRepository, IRepository<Book, Guid> bookRepository)
        {

            _transactionDetailRepository = transactionDetailRepository;
            _transactionDetailManager = transactionDetailManager; _transactionRepository = transactionRepository;
            _bookRepository = bookRepository;
        }

        public virtual async Task<PagedResultDto<TransactionDetailWithNavigationPropertiesDto>> GetListAsync(GetTransactionDetailsInput input)
        {
            var totalCount = await _transactionDetailRepository.GetCountAsync(input.FilterText, input.QuantityMin, input.QuantityMax, input.SubTotalMin, input.SubTotalMax, input.TransactionId, input.BookId);
            var items = await _transactionDetailRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.QuantityMin, input.QuantityMax, input.SubTotalMin, input.SubTotalMax, input.TransactionId, input.BookId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<TransactionDetailWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<TransactionDetailWithNavigationProperties>, List<TransactionDetailWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<TransactionDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<TransactionDetailWithNavigationProperties, TransactionDetailWithNavigationPropertiesDto>
                (await _transactionDetailRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<TransactionDetailDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<TransactionDetail, TransactionDetailDto>(await _transactionDetailRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetTransactionLookupAsync(LookupRequestDto input)
        {
            var query = (await _transactionRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.PaymentStatus != null &&
                         x.PaymentStatus.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Transaction>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Transaction>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetBookLookupAsync(LookupRequestDto input)
        {
            var query = (await _bookRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Title != null &&
                         x.Title.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Book>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Book>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(ProductManagementPermissions.TransactionDetails.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _transactionDetailRepository.DeleteAsync(id);
        }

        [Authorize(ProductManagementPermissions.TransactionDetails.Create)]
        public virtual async Task<TransactionDetailDto> CreateAsync(TransactionDetailCreateDto input)
        {

            var transactionDetail = await _transactionDetailManager.CreateAsync(
            input.TransactionId, input.BookId, input.Quantity, input.SubTotal
            );

            return ObjectMapper.Map<TransactionDetail, TransactionDetailDto>(transactionDetail);
        }

        [Authorize(ProductManagementPermissions.TransactionDetails.Edit)]
        public virtual async Task<TransactionDetailDto> UpdateAsync(Guid id, TransactionDetailUpdateDto input)
        {

            var transactionDetail = await _transactionDetailManager.UpdateAsync(
            id,
            input.TransactionId, input.BookId, input.Quantity, input.SubTotal, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<TransactionDetail, TransactionDetailDto>(transactionDetail);
        }
    }
}