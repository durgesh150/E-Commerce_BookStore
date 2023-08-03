using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ProductManagement.TransactionDetails
{
    public class TransactionDetailsAppServiceTests : ProductManagementApplicationTestBase
    {
        private readonly ITransactionDetailsAppService _transactionDetailsAppService;
        private readonly IRepository<TransactionDetail, Guid> _transactionDetailRepository;

        public TransactionDetailsAppServiceTests()
        {
            _transactionDetailsAppService = GetRequiredService<ITransactionDetailsAppService>();
            _transactionDetailRepository = GetRequiredService<IRepository<TransactionDetail, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _transactionDetailsAppService.GetListAsync(new GetTransactionDetailsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.TransactionDetail.Id == Guid.Parse("1fb3f283-5af1-4729-8e07-ecf3a4e5ec8d")).ShouldBe(true);
            result.Items.Any(x => x.TransactionDetail.Id == Guid.Parse("e4214abd-032d-46cb-ad74-d12777bcf4b2")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _transactionDetailsAppService.GetAsync(Guid.Parse("1fb3f283-5af1-4729-8e07-ecf3a4e5ec8d"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("1fb3f283-5af1-4729-8e07-ecf3a4e5ec8d"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new TransactionDetailCreateDto
            {
                Quantity = 587631380,
                SubTotal = 1148906310
            };

            // Act
            var serviceResult = await _transactionDetailsAppService.CreateAsync(input);

            // Assert
            var result = await _transactionDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Quantity.ShouldBe(587631380);
            result.SubTotal.ShouldBe(1148906310);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new TransactionDetailUpdateDto()
            {
                Quantity = 1319506706,
                SubTotal = 1290032491
            };

            // Act
            var serviceResult = await _transactionDetailsAppService.UpdateAsync(Guid.Parse("1fb3f283-5af1-4729-8e07-ecf3a4e5ec8d"), input);

            // Assert
            var result = await _transactionDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Quantity.ShouldBe(1319506706);
            result.SubTotal.ShouldBe(1290032491);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _transactionDetailsAppService.DeleteAsync(Guid.Parse("1fb3f283-5af1-4729-8e07-ecf3a4e5ec8d"));

            // Assert
            var result = await _transactionDetailRepository.FindAsync(c => c.Id == Guid.Parse("1fb3f283-5af1-4729-8e07-ecf3a4e5ec8d"));

            result.ShouldBeNull();
        }
    }
}