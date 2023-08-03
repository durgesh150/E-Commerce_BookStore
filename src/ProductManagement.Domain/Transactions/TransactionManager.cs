using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ProductManagement.Transactions
{
    public class TransactionManager : DomainService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionManager(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<Transaction> CreateAsync(
        Guid userId, DateTime transactionDate, decimal totalAmount, string paymentStatus)
        {
            Check.NotNull(transactionDate, nameof(transactionDate));
            Check.NotNullOrWhiteSpace(paymentStatus, nameof(paymentStatus));

            var transaction = new Transaction(
             GuidGenerator.Create(),
             userId, transactionDate, totalAmount, paymentStatus
             );

            return await _transactionRepository.InsertAsync(transaction);
        }

        public async Task<Transaction> UpdateAsync(
            Guid id,
            Guid userId, DateTime transactionDate, decimal totalAmount, string paymentStatus, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(transactionDate, nameof(transactionDate));
            Check.NotNullOrWhiteSpace(paymentStatus, nameof(paymentStatus));

            var transaction = await _transactionRepository.GetAsync(id);

            transaction.UserId = userId;
            transaction.TransactionDate = transactionDate;
            transaction.TotalAmount = totalAmount;
            transaction.PaymentStatus = paymentStatus;

            transaction.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _transactionRepository.UpdateAsync(transaction);
        }

    }
}