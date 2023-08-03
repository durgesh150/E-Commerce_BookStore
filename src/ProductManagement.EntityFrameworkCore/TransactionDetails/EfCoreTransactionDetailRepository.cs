using ProductManagement.Books;
using ProductManagement.Transactions;
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

namespace ProductManagement.TransactionDetails
{
    public class EfCoreTransactionDetailRepository : EfCoreRepository<ProductManagementDbContext, TransactionDetail, Guid>, ITransactionDetailRepository
    {
        public EfCoreTransactionDetailRepository(IDbContextProvider<ProductManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<TransactionDetailWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(transactionDetail => new TransactionDetailWithNavigationProperties
                {
                    TransactionDetail = transactionDetail,
                    Transaction = dbContext.Set<Transaction>().FirstOrDefault(c => c.Id == transactionDetail.TransactionId),
                    Book = dbContext.Set<Book>().FirstOrDefault(c => c.Id == transactionDetail.BookId)
                }).FirstOrDefault();
        }

        public async Task<List<TransactionDetailWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            int? quantityMin = null,
            int? quantityMax = null,
            decimal? subTotalMin = null,
            decimal? subTotalMax = null,
            Guid? transactionId = null,
            Guid? bookId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, quantityMin, quantityMax, subTotalMin, subTotalMax, transactionId, bookId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TransactionDetailConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<TransactionDetailWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from transactionDetail in (await GetDbSetAsync())
                   join transaction in (await GetDbContextAsync()).Set<Transaction>() on transactionDetail.TransactionId equals transaction.Id into transactions
                   from transaction in transactions.DefaultIfEmpty()
                   join book in (await GetDbContextAsync()).Set<Book>() on transactionDetail.BookId equals book.Id into books
                   from book in books.DefaultIfEmpty()
                   select new TransactionDetailWithNavigationProperties
                   {
                       TransactionDetail = transactionDetail,
                       Transaction = transaction,
                       Book = book
                   };
        }

        protected virtual IQueryable<TransactionDetailWithNavigationProperties> ApplyFilter(
            IQueryable<TransactionDetailWithNavigationProperties> query,
            string filterText,
            int? quantityMin = null,
            int? quantityMax = null,
            decimal? subTotalMin = null,
            decimal? subTotalMax = null,
            Guid? transactionId = null,
            Guid? bookId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(quantityMin.HasValue, e => e.TransactionDetail.Quantity >= quantityMin.Value)
                    .WhereIf(quantityMax.HasValue, e => e.TransactionDetail.Quantity <= quantityMax.Value)
                    .WhereIf(subTotalMin.HasValue, e => e.TransactionDetail.SubTotal >= subTotalMin.Value)
                    .WhereIf(subTotalMax.HasValue, e => e.TransactionDetail.SubTotal <= subTotalMax.Value)
                    .WhereIf(transactionId != null && transactionId != Guid.Empty, e => e.Transaction != null && e.Transaction.Id == transactionId)
                    .WhereIf(bookId != null && bookId != Guid.Empty, e => e.Book != null && e.Book.Id == bookId);
        }

        public async Task<List<TransactionDetail>> GetListAsync(
            string filterText = null,
            int? quantityMin = null,
            int? quantityMax = null,
            decimal? subTotalMin = null,
            decimal? subTotalMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, quantityMin, quantityMax, subTotalMin, subTotalMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TransactionDetailConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            int? quantityMin = null,
            int? quantityMax = null,
            decimal? subTotalMin = null,
            decimal? subTotalMax = null,
            Guid? transactionId = null,
            Guid? bookId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, quantityMin, quantityMax, subTotalMin, subTotalMax, transactionId, bookId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<TransactionDetail> ApplyFilter(
            IQueryable<TransactionDetail> query,
            string filterText,
            int? quantityMin = null,
            int? quantityMax = null,
            decimal? subTotalMin = null,
            decimal? subTotalMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(quantityMin.HasValue, e => e.Quantity >= quantityMin.Value)
                    .WhereIf(quantityMax.HasValue, e => e.Quantity <= quantityMax.Value)
                    .WhereIf(subTotalMin.HasValue, e => e.SubTotal >= subTotalMin.Value)
                    .WhereIf(subTotalMax.HasValue, e => e.SubTotal <= subTotalMax.Value);
        }
    }
}