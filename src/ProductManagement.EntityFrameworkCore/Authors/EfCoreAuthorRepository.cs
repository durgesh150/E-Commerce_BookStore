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

namespace ProductManagement.Authors
{
    public class EfCoreAuthorRepository : EfCoreRepository<ProductManagementDbContext, Author, Guid>, IAuthorRepository
    {
        public EfCoreAuthorRepository(IDbContextProvider<ProductManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Author>> GetListAsync(
            string filterText = null,
            string firstName = null,
            string lastName = null,
            string email = null,
            string bio = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, firstName, lastName, email, bio);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AuthorConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string firstName = null,
            string lastName = null,
            string email = null,
            string bio = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, firstName, lastName, email, bio);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Author> ApplyFilter(
            IQueryable<Author> query,
            string filterText,
            string firstName = null,
            string lastName = null,
            string email = null,
            string bio = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.FirstName.Contains(filterText) || e.LastName.Contains(filterText) || e.Email.Contains(filterText) || e.Bio.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(firstName), e => e.FirstName.Contains(firstName))
                    .WhereIf(!string.IsNullOrWhiteSpace(lastName), e => e.LastName.Contains(lastName))
                    .WhereIf(!string.IsNullOrWhiteSpace(email), e => e.Email.Contains(email))
                    .WhereIf(!string.IsNullOrWhiteSpace(bio), e => e.Bio.Contains(bio));
        }
    }
}