using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ProductManagement.Carts
{
    public class CartsAppServiceTests : ProductManagementApplicationTestBase
    {
        private readonly ICartsAppService _cartsAppService;
        private readonly IRepository<Cart, Guid> _cartRepository;

        public CartsAppServiceTests()
        {
            _cartsAppService = GetRequiredService<ICartsAppService>();
            _cartRepository = GetRequiredService<IRepository<Cart, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _cartsAppService.GetListAsync(new GetCartsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Cart.Id == Guid.Parse("120b15bf-0943-46cd-8906-c0624cb39a4c")).ShouldBe(true);
            result.Items.Any(x => x.Cart.Id == Guid.Parse("9db0d161-312e-4c71-a051-479d14d30c13")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _cartsAppService.GetAsync(Guid.Parse("120b15bf-0943-46cd-8906-c0624cb39a4c"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("120b15bf-0943-46cd-8906-c0624cb39a4c"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CartCreateDto
            {
                UserId = Guid.Parse("cbe363b6-8c89-4cbb-b108-4e04b21f2d8c"),
                Quantity = 825440422,
                DateAdded = new DateTime(2001, 8, 27),
                UnitPrice = 1850559264,
                TotalPrice = 649658890,
                LastModified = new DateTime(2015, 6, 3),
                BookId = Guid.Parse("579dc4c9-914b-4e9f-8055-6ad977f48814")
            };

            // Act
            var serviceResult = await _cartsAppService.CreateAsync(input);

            // Assert
            var result = await _cartRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.UserId.ShouldBe(Guid.Parse("cbe363b6-8c89-4cbb-b108-4e04b21f2d8c"));
            result.Quantity.ShouldBe(825440422);
            result.DateAdded.ShouldBe(new DateTime(2001, 8, 27));
            result.UnitPrice.ShouldBe(1850559264);
            result.TotalPrice.ShouldBe(649658890);
            result.LastModified.ShouldBe(new DateTime(2015, 6, 3));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CartUpdateDto()
            {
                UserId = Guid.Parse("051a0288-94dc-439c-8c9e-f474dbb93bf4"),
                Quantity = 2028084689,
                DateAdded = new DateTime(2006, 5, 2),
                UnitPrice = 1846321379,
                TotalPrice = 1783497565,
                LastModified = new DateTime(2006, 7, 14),
                BookId = Guid.Parse("579dc4c9-914b-4e9f-8055-6ad977f48814")
            };

            // Act
            var serviceResult = await _cartsAppService.UpdateAsync(Guid.Parse("120b15bf-0943-46cd-8906-c0624cb39a4c"), input);

            // Assert
            var result = await _cartRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.UserId.ShouldBe(Guid.Parse("051a0288-94dc-439c-8c9e-f474dbb93bf4"));
            result.Quantity.ShouldBe(2028084689);
            result.DateAdded.ShouldBe(new DateTime(2006, 5, 2));
            result.UnitPrice.ShouldBe(1846321379);
            result.TotalPrice.ShouldBe(1783497565);
            result.LastModified.ShouldBe(new DateTime(2006, 7, 14));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _cartsAppService.DeleteAsync(Guid.Parse("120b15bf-0943-46cd-8906-c0624cb39a4c"));

            // Assert
            var result = await _cartRepository.FindAsync(c => c.Id == Guid.Parse("120b15bf-0943-46cd-8906-c0624cb39a4c"));

            result.ShouldBeNull();
        }
    }
}