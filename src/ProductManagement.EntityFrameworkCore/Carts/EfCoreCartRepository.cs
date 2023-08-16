using ProductManagement.Books;
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

namespace ProductManagement.Carts
{
    public class EfCoreCartRepository : EfCoreRepository<ProductManagementDbContext, Cart, Guid>, ICartRepository
    {
        public EfCoreCartRepository(IDbContextProvider<ProductManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<CartWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(cart => new CartWithNavigationProperties
                {
                    Cart = cart,
                    Book = dbContext.Set<Book>().FirstOrDefault(c => c.Id == cart.BookId)
                }).FirstOrDefault();
        }

        public async Task<List<CartWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            Guid? userId = null,
            int? quantityMin = null,
            int? quantityMax = null,
            DateTime? dateAddedMin = null,
            DateTime? dateAddedMax = null,
            decimal? unitPriceMin = null,
            decimal? unitPriceMax = null,
            decimal? totalPriceMin = null,
            decimal? totalPriceMax = null,
            DateTime? lastModifiedMin = null,
            DateTime? lastModifiedMax = null,
            Guid? bookId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, userId, quantityMin, quantityMax, dateAddedMin, dateAddedMax, unitPriceMin, unitPriceMax, totalPriceMin, totalPriceMax, lastModifiedMin, lastModifiedMax, bookId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CartConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CartWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from cart in (await GetDbSetAsync())
                   join book in (await GetDbContextAsync()).Set<Book>() on cart.BookId equals book.Id into books
                   from book in books.DefaultIfEmpty()
                   select new CartWithNavigationProperties
                   {
                       Cart = cart,
                       Book = book
                   };
        }

        protected virtual IQueryable<CartWithNavigationProperties> ApplyFilter(
            IQueryable<CartWithNavigationProperties> query,
            string filterText,
            Guid? userId = null,
            int? quantityMin = null,
            int? quantityMax = null,
            DateTime? dateAddedMin = null,
            DateTime? dateAddedMax = null,
            decimal? unitPriceMin = null,
            decimal? unitPriceMax = null,
            decimal? totalPriceMin = null,
            decimal? totalPriceMax = null,
            DateTime? lastModifiedMin = null,
            DateTime? lastModifiedMax = null,
            Guid? bookId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(userId.HasValue, e => e.Cart.UserId == userId)
                    .WhereIf(quantityMin.HasValue, e => e.Cart.Quantity >= quantityMin.Value)
                    .WhereIf(quantityMax.HasValue, e => e.Cart.Quantity <= quantityMax.Value)
                    .WhereIf(dateAddedMin.HasValue, e => e.Cart.DateAdded >= dateAddedMin.Value)
                    .WhereIf(dateAddedMax.HasValue, e => e.Cart.DateAdded <= dateAddedMax.Value)
                    .WhereIf(unitPriceMin.HasValue, e => e.Cart.UnitPrice >= unitPriceMin.Value)
                    .WhereIf(unitPriceMax.HasValue, e => e.Cart.UnitPrice <= unitPriceMax.Value)
                    .WhereIf(totalPriceMin.HasValue, e => e.Cart.TotalPrice >= totalPriceMin.Value)
                    .WhereIf(totalPriceMax.HasValue, e => e.Cart.TotalPrice <= totalPriceMax.Value)
                    .WhereIf(lastModifiedMin.HasValue, e => e.Cart.LastModified >= lastModifiedMin.Value)
                    .WhereIf(lastModifiedMax.HasValue, e => e.Cart.LastModified <= lastModifiedMax.Value)
                    .WhereIf(bookId != null && bookId != Guid.Empty, e => e.Book != null && e.Book.Id == bookId);
        }

        public async Task<List<Cart>> GetListAsync(
            string filterText = null,
            Guid? userId = null,
            int? quantityMin = null,
            int? quantityMax = null,
            DateTime? dateAddedMin = null,
            DateTime? dateAddedMax = null,
            decimal? unitPriceMin = null,
            decimal? unitPriceMax = null,
            decimal? totalPriceMin = null,
            decimal? totalPriceMax = null,
            DateTime? lastModifiedMin = null,
            DateTime? lastModifiedMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, userId, quantityMin, quantityMax, dateAddedMin, dateAddedMax, unitPriceMin, unitPriceMax, totalPriceMin, totalPriceMax, lastModifiedMin, lastModifiedMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CartConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            Guid? userId = null,
            int? quantityMin = null,
            int? quantityMax = null,
            DateTime? dateAddedMin = null,
            DateTime? dateAddedMax = null,
            decimal? unitPriceMin = null,
            decimal? unitPriceMax = null,
            decimal? totalPriceMin = null,
            decimal? totalPriceMax = null,
            DateTime? lastModifiedMin = null,
            DateTime? lastModifiedMax = null,
            Guid? bookId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, userId, quantityMin, quantityMax, dateAddedMin, dateAddedMax, unitPriceMin, unitPriceMax, totalPriceMin, totalPriceMax, lastModifiedMin, lastModifiedMax, bookId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Cart> ApplyFilter(
            IQueryable<Cart> query,
            string filterText,
            Guid? userId = null,
            int? quantityMin = null,
            int? quantityMax = null,
            DateTime? dateAddedMin = null,
            DateTime? dateAddedMax = null,
            decimal? unitPriceMin = null,
            decimal? unitPriceMax = null,
            decimal? totalPriceMin = null,
            decimal? totalPriceMax = null,
            DateTime? lastModifiedMin = null,
            DateTime? lastModifiedMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(userId.HasValue, e => e.UserId == userId)
                    .WhereIf(quantityMin.HasValue, e => e.Quantity >= quantityMin.Value)
                    .WhereIf(quantityMax.HasValue, e => e.Quantity <= quantityMax.Value)
                    .WhereIf(dateAddedMin.HasValue, e => e.DateAdded >= dateAddedMin.Value)
                    .WhereIf(dateAddedMax.HasValue, e => e.DateAdded <= dateAddedMax.Value)
                    .WhereIf(unitPriceMin.HasValue, e => e.UnitPrice >= unitPriceMin.Value)
                    .WhereIf(unitPriceMax.HasValue, e => e.UnitPrice <= unitPriceMax.Value)
                    .WhereIf(totalPriceMin.HasValue, e => e.TotalPrice >= totalPriceMin.Value)
                    .WhereIf(totalPriceMax.HasValue, e => e.TotalPrice <= totalPriceMax.Value)
                    .WhereIf(lastModifiedMin.HasValue, e => e.LastModified >= lastModifiedMin.Value)
                    .WhereIf(lastModifiedMax.HasValue, e => e.LastModified <= lastModifiedMax.Value);
        }
    }
}