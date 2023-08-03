using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ProductManagement.TransactionDetails;
using ProductManagement.EntityFrameworkCore;
using Xunit;

namespace ProductManagement.TransactionDetails
{
    public class TransactionDetailRepositoryTests : ProductManagementEntityFrameworkCoreTestBase
    {
        private readonly ITransactionDetailRepository _transactionDetailRepository;

        public TransactionDetailRepositoryTests()
        {
            _transactionDetailRepository = GetRequiredService<ITransactionDetailRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _transactionDetailRepository.GetListAsync(

                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("1fb3f283-5af1-4729-8e07-ecf3a4e5ec8d"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _transactionDetailRepository.GetCountAsync(

                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}