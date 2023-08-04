using ProductManagement;
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

namespace ProductManagement.Addresses
{
    public class EfCoreAddressRepository : EfCoreRepository<ProductManagementDbContext, Address, Guid>, IAddressRepository
    {
        public EfCoreAddressRepository(IDbContextProvider<ProductManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Address>> GetListAsync(
            string filterText = null,
            string cIty = null,
            string state = null,
            long? postalCodeMin = null,
            long? postalCodeMax = null,
            Country? country = null,
            Guid? userId = null,
            string streetAddress = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, cIty, state, postalCodeMin, postalCodeMax, country, userId, streetAddress);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AddressConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string cIty = null,
            string state = null,
            long? postalCodeMin = null,
            long? postalCodeMax = null,
            Country? country = null,
            Guid? userId = null,
            string streetAddress = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, cIty, state, postalCodeMin, postalCodeMax, country, userId, streetAddress);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Address> ApplyFilter(
            IQueryable<Address> query,
            string filterText,
            string cIty = null,
            string state = null,
            long? postalCodeMin = null,
            long? postalCodeMax = null,
            Country? country = null,
            Guid? userId = null,
            string streetAddress = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.CIty.Contains(filterText) || e.State.Contains(filterText) || e.StreetAddress.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(cIty), e => e.CIty.Contains(cIty))
                    .WhereIf(!string.IsNullOrWhiteSpace(state), e => e.State.Contains(state))
                    .WhereIf(postalCodeMin.HasValue, e => e.PostalCode >= postalCodeMin.Value)
                    .WhereIf(postalCodeMax.HasValue, e => e.PostalCode <= postalCodeMax.Value)
                    .WhereIf(country.HasValue, e => e.Country == country)
                    .WhereIf(userId.HasValue, e => e.UserId == userId)
                    .WhereIf(!string.IsNullOrWhiteSpace(streetAddress), e => e.StreetAddress.Contains(streetAddress));
        }
    }
}