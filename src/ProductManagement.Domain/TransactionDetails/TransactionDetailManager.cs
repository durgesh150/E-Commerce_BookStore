using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ProductManagement.TransactionDetails
{
    public class TransactionDetailManager : DomainService
    {
        private readonly ITransactionDetailRepository _transactionDetailRepository;

        public TransactionDetailManager(ITransactionDetailRepository transactionDetailRepository)
        {
            _transactionDetailRepository = transactionDetailRepository;
        }

        public async Task<TransactionDetail> CreateAsync(
        Guid? transactionId, Guid? bookId, int quantity, decimal subTotal)
        {

            var transactionDetail = new TransactionDetail(
             GuidGenerator.Create(),
             transactionId, bookId, quantity, subTotal
             );

            return await _transactionDetailRepository.InsertAsync(transactionDetail);
        }

        public async Task<TransactionDetail> UpdateAsync(
            Guid id,
            Guid? transactionId, Guid? bookId, int quantity, decimal subTotal, [CanBeNull] string concurrencyStamp = null
        )
        {

            var transactionDetail = await _transactionDetailRepository.GetAsync(id);

            transactionDetail.TransactionId = transactionId;
            transactionDetail.BookId = bookId;
            transactionDetail.Quantity = quantity;
            transactionDetail.SubTotal = subTotal;

            transactionDetail.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _transactionDetailRepository.UpdateAsync(transactionDetail);
        }

    }
}