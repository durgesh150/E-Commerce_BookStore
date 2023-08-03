using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ProductManagement.Authors
{
    public interface IAuthorRepository : IRepository<Author, Guid>
    {
        Task<List<Author>> GetListAsync(
            string filterText = null,
            string firstName = null,
            string lastName = null,
            string email = null,
            string bio = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string firstName = null,
            string lastName = null,
            string email = null,
            string bio = null,
            CancellationToken cancellationToken = default);
    }
}