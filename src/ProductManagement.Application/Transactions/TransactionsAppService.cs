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
using ProductManagement.Transactions;

namespace ProductManagement.Transactions
{

    [Authorize(ProductManagementPermissions.Transactions.Default)]
    public class TransactionsAppService : ApplicationService, ITransactionsAppService
    {

        private readonly ITransactionRepository _transactionRepository;
        private readonly TransactionManager _transactionManager;

        public TransactionsAppService(ITransactionRepository transactionRepository, TransactionManager transactionManager)
        {

            _transactionRepository = transactionRepository;
            _transactionManager = transactionManager;
        }

        public virtual async Task<PagedResultDto<TransactionDto>> GetListAsync(GetTransactionsInput input)
        {
            var totalCount = await _transactionRepository.GetCountAsync(input.FilterText, input.UserId, input.TransactionDateMin, input.TransactionDateMax, input.TotalAmountMin, input.TotalAmountMax, input.PaymentStatus);
            var items = await _transactionRepository.GetListAsync(input.FilterText, input.UserId, input.TransactionDateMin, input.TransactionDateMax, input.TotalAmountMin, input.TotalAmountMax, input.PaymentStatus, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<TransactionDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Transaction>, List<TransactionDto>>(items)
            };
        }

        public virtual async Task<TransactionDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Transaction, TransactionDto>(await _transactionRepository.GetAsync(id));
        }

        [Authorize(ProductManagementPermissions.Transactions.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _transactionRepository.DeleteAsync(id);
        }

        [Authorize(ProductManagementPermissions.Transactions.Create)]
        public virtual async Task<TransactionDto> CreateAsync(TransactionCreateDto input)
        {

            var transaction = await _transactionManager.CreateAsync(
            input.UserId, input.TransactionDate, input.TotalAmount, input.PaymentStatus
            );

            return ObjectMapper.Map<Transaction, TransactionDto>(transaction);
        }

        [Authorize(ProductManagementPermissions.Transactions.Edit)]
        public virtual async Task<TransactionDto> UpdateAsync(Guid id, TransactionUpdateDto input)
        {

            var transaction = await _transactionManager.UpdateAsync(
            id,
            input.UserId, input.TransactionDate, input.TotalAmount, input.PaymentStatus, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Transaction, TransactionDto>(transaction);
        }
    }
}