using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ProductManagement.Transactions;
using ProductManagement.EntityFrameworkCore;
using Xunit;

namespace ProductManagement.Transactions
{
    public class TransactionRepositoryTests : ProductManagementEntityFrameworkCoreTestBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionRepositoryTests()
        {
            _transactionRepository = GetRequiredService<ITransactionRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _transactionRepository.GetListAsync(
                    userId: Guid.Parse("0250f38e-af64-4ee4-805f-ff2af04caa8f"),
                    paymentStatus: "d48b38b7381f4478a5e8ae5de68fe506b7c371bf0bed45d08d90fac9ab9d9ea82490962c"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("733d4c43-43f2-4989-a454-f7a677d52377"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _transactionRepository.GetCountAsync(
                    userId: Guid.Parse("97d1ca1f-f80a-4cc6-83c7-485a1cd62f9e"),
                    paymentStatus: "657b993517ad4b289634ef40dba48da2d143e93203c64014bf66e011f9495e5295cad2e30355473fad4e"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}