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
                    userId: Guid.Parse("3242797a-2978-4259-a65b-a99b0669b19f")
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("120b15bf-0943-46cd-8906-c0624cb39a4c"));
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
                    userId: Guid.Parse("b8c7509f-7a95-4ab5-a0be-00c36118da3a")
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}