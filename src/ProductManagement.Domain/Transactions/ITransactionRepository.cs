using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ProductManagement.Transactions
{
    public interface ITransactionRepository : IRepository<Transaction, Guid>
    {
        Task<List<Transaction>> GetListAsync(
            string filterText = null,
            Guid? userId = null,
            DateTime? transactionDateMin = null,
            DateTime? transactionDateMax = null,
            decimal? totalAmountMin = null,
            decimal? totalAmountMax = null,
            string paymentStatus = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            Guid? userId = null,
            DateTime? transactionDateMin = null,
            DateTime? transactionDateMax = null,
            decimal? totalAmountMin = null,
            decimal? totalAmountMax = null,
            string paymentStatus = null,
            CancellationToken cancellationToken = default);
    }
}