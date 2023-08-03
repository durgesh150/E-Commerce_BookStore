using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ProductManagement.Transactions
{
    public class TransactionsAppServiceTests : ProductManagementApplicationTestBase
    {
        private readonly ITransactionsAppService _transactionsAppService;
        private readonly IRepository<Transaction, Guid> _transactionRepository;

        public TransactionsAppServiceTests()
        {
            _transactionsAppService = GetRequiredService<ITransactionsAppService>();
            _transactionRepository = GetRequiredService<IRepository<Transaction, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _transactionsAppService.GetListAsync(new GetTransactionsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("733d4c43-43f2-4989-a454-f7a677d52377")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("de8fefa2-8b57-4c74-8741-1172e87489ee")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _transactionsAppService.GetAsync(Guid.Parse("733d4c43-43f2-4989-a454-f7a677d52377"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("733d4c43-43f2-4989-a454-f7a677d52377"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new TransactionCreateDto
            {
                UserId = Guid.Parse("f29f2d9f-c2af-49aa-ada0-6e6ed7a9d27d"),
                TransactionDate = new DateTime(2017, 9, 1),
                TotalAmount = 1800617427,
                PaymentStatus = "5d11faf247374edc881280803a2069f9700f9e96b0cc4e72bf896b5fa67c63df5fb"
            };

            // Act
            var serviceResult = await _transactionsAppService.CreateAsync(input);

            // Assert
            var result = await _transactionRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.UserId.ShouldBe(Guid.Parse("f29f2d9f-c2af-49aa-ada0-6e6ed7a9d27d"));
            result.TransactionDate.ShouldBe(new DateTime(2017, 9, 1));
            result.TotalAmount.ShouldBe(1800617427);
            result.PaymentStatus.ShouldBe("5d11faf247374edc881280803a2069f9700f9e96b0cc4e72bf896b5fa67c63df5fb");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new TransactionUpdateDto()
            {
                UserId = Guid.Parse("e6db2cce-ca0f-48d1-b14f-ece6fe960362"),
                TransactionDate = new DateTime(2015, 9, 23),
                TotalAmount = 56092444,
                PaymentStatus = "1b18963fd5a14ec99a3f8ebc57b6cd4a076b9ea8e3444235ae676b1fe969fc880739603bdfb945b495d7f90fb0"
            };

            // Act
            var serviceResult = await _transactionsAppService.UpdateAsync(Guid.Parse("733d4c43-43f2-4989-a454-f7a677d52377"), input);

            // Assert
            var result = await _transactionRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.UserId.ShouldBe(Guid.Parse("e6db2cce-ca0f-48d1-b14f-ece6fe960362"));
            result.TransactionDate.ShouldBe(new DateTime(2015, 9, 23));
            result.TotalAmount.ShouldBe(56092444);
            result.PaymentStatus.ShouldBe("1b18963fd5a14ec99a3f8ebc57b6cd4a076b9ea8e3444235ae676b1fe969fc880739603bdfb945b495d7f90fb0");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _transactionsAppService.DeleteAsync(Guid.Parse("733d4c43-43f2-4989-a454-f7a677d52377"));

            // Assert
            var result = await _transactionRepository.FindAsync(c => c.Id == Guid.Parse("733d4c43-43f2-4989-a454-f7a677d52377"));

            result.ShouldBeNull();
        }
    }
}