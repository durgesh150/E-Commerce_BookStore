using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using ProductManagement.EntityFrameworkCore;

namespace ProductManagement.Transactions
{
    public class EfCoreTransactionRepository : EfCoreRepository<ProductManagementDbContext, Transaction, Guid>, ITransactionRepository
    {
        public EfCoreTransactionRepository(IDbContextProvider<ProductManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Transaction>> GetListAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, userId, transactionDateMin, transactionDateMax, totalAmountMin, totalAmountMax, paymentStatus);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TransactionConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            Guid? userId = null,
            DateTime? transactionDateMin = null,
            DateTime? transactionDateMax = null,
            decimal? totalAmountMin = null,
            decimal? totalAmountMax = null,
            string paymentStatus = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, userId, transactionDateMin, transactionDateMax, totalAmountMin, totalAmountMax, paymentStatus);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Transaction> ApplyFilter(
            IQueryable<Transaction> query,
            string filterText,
            Guid? userId = null,
            DateTime? transactionDateMin = null,
            DateTime? transactionDateMax = null,
            decimal? totalAmountMin = null,
            decimal? totalAmountMax = null,
            string paymentStatus = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.PaymentStatus.Contains(filterText))
                    .WhereIf(userId.HasValue, e => e.UserId == userId)
                    .WhereIf(transactionDateMin.HasValue, e => e.TransactionDate >= transactionDateMin.Value)
                    .WhereIf(transactionDateMax.HasValue, e => e.TransactionDate <= transactionDateMax.Value)
                    .WhereIf(totalAmountMin.HasValue, e => e.TotalAmount >= totalAmountMin.Value)
                    .WhereIf(totalAmountMax.HasValue, e => e.TotalAmount <= totalAmountMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(paymentStatus), e => e.PaymentStatus.Contains(paymentStatus));
        }
    }
}