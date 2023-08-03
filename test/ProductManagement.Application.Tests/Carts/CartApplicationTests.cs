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
            result.Items.Any(x => x.Cart.Id == Guid.Parse("c8e464fe-9290-4138-b7dc-5ab17c28e42e")).ShouldBe(true);
            result.Items.Any(x => x.Cart.Id == Guid.Parse("fa496b8d-9b1b-4a52-ab9f-4699663b7127")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _cartsAppService.GetAsync(Guid.Parse("c8e464fe-9290-4138-b7dc-5ab17c28e42e"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("c8e464fe-9290-4138-b7dc-5ab17c28e42e"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CartCreateDto
            {
                UserId = Guid.Parse("039ca416-0747-47e0-9885-32cea8cfac50"),
                Quantity = 790330803,
                DateAdded = new DateTime(2015, 3, 21)
            };

            // Act
            var serviceResult = await _cartsAppService.CreateAsync(input);

            // Assert
            var result = await _cartRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.UserId.ShouldBe(Guid.Parse("039ca416-0747-47e0-9885-32cea8cfac50"));
            result.Quantity.ShouldBe(790330803);
            result.DateAdded.ShouldBe(new DateTime(2015, 3, 21));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CartUpdateDto()
            {
                UserId = Guid.Parse("894849f6-2477-42b8-bea0-1bc2c553035a"),
                Quantity = 618006782,
                DateAdded = new DateTime(2003, 2, 6)
            };

            // Act
            var serviceResult = await _cartsAppService.UpdateAsync(Guid.Parse("c8e464fe-9290-4138-b7dc-5ab17c28e42e"), input);

            // Assert
            var result = await _cartRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.UserId.ShouldBe(Guid.Parse("894849f6-2477-42b8-bea0-1bc2c553035a"));
            result.Quantity.ShouldBe(618006782);
            result.DateAdded.ShouldBe(new DateTime(2003, 2, 6));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _cartsAppService.DeleteAsync(Guid.Parse("c8e464fe-9290-4138-b7dc-5ab17c28e42e"));

            // Assert
            var result = await _cartRepository.FindAsync(c => c.Id == Guid.Parse("c8e464fe-9290-4138-b7dc-5ab17c28e42e"));

            result.ShouldBeNull();
        }
    }
}