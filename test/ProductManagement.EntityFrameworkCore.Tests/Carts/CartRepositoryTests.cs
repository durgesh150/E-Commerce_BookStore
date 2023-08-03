using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ProductManagement.Carts;
using ProductManagement.EntityFrameworkCore;
using Xunit;

namespace ProductManagement.Carts
{
    public class CartRepositoryTests : ProductManagementEntityFrameworkCoreTestBase
    {
        private readonly ICartRepository _cartRepository;

        public CartRepositoryTests()
        {
            _cartRepository = GetRequiredService<ICartRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _cartRepository.GetListAsync(
                    userId: Guid.Parse("58263738-e001-4d79-80af-775b907da915")
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("c8e464fe-9290-4138-b7dc-5ab17c28e42e"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _cartRepository.GetCountAsync(
                    userId: Guid.Parse("e08b1d6b-2610-42af-8614-fdc618a7c463")
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}