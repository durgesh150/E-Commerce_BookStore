using ProductManagement;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ProductManagement.Addresses
{
    public interface IAddressRepository : IRepository<Address, Guid>
    {
        Task<List<Address>> GetListAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string cIty = null,
            string state = null,
            long? postalCodeMin = null,
            long? postalCodeMax = null,
            Country? country = null,
            Guid? userId = null,
            string streetAddress = null,
            CancellationToken cancellationToken = default);
    }
}