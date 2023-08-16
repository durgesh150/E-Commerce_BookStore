using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ProductManagement.Carts
{
    public interface ICartRepository : IRepository<Cart, Guid>
    {
        Task<CartWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<CartWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<List<Cart>> GetListAsync(
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
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default);
    }
}