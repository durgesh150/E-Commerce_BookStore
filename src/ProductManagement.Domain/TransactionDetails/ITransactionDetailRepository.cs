using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ProductManagement.TransactionDetails
{
    public interface ITransactionDetailRepository : IRepository<TransactionDetail, Guid>
    {
        Task<TransactionDetailWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<TransactionDetailWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<List<TransactionDetail>> GetListAsync(
                    string filterText = null,
                    int? quantityMin = null,
                    int? quantityMax = null,
                    decimal? subTotalMin = null,
                    decimal? subTotalMax = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            int? quantityMin = null,
            int? quantityMax = null,
            decimal? subTotalMin = null,
            decimal? subTotalMax = null,
            Guid? transactionId = null,
            Guid? bookId = null,
            CancellationToken cancellationToken = default);
    }
}