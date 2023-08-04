using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ProductManagement.Addresses
{
    public class AddressesAppServiceTests : ProductManagementApplicationTestBase
    {
        private readonly IAddressesAppService _addressesAppService;
        private readonly IRepository<Address, Guid> _addressRepository;

        public AddressesAppServiceTests()
        {
            _addressesAppService = GetRequiredService<IAddressesAppService>();
            _addressRepository = GetRequiredService<IRepository<Address, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _addressesAppService.GetListAsync(new GetAddressesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("96f66c43-719a-4748-837b-c25ef764ba42")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("22d84528-8dfc-4380-ae30-69878eb8eb64")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _addressesAppService.GetAsync(Guid.Parse("96f66c43-719a-4748-837b-c25ef764ba42"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("96f66c43-719a-4748-837b-c25ef764ba42"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new AddressCreateDto
            {
                CIty = "d8626c6",
                State = "1f125cb610e941e992ea122a6680bc930ede71b4a54d4a6ca81b166ce0fcfe67cd57c9c996af449e8953787cb5f02",
                PostalCode = 7,
                Country = default,
                UserId = Guid.Parse("524ae472-ffbb-4f33-a564-0c4723707594"),
                StreetAddress = "7268f9bc3c1e4af49952696d0d1e3a3d191a6c8619f346f79a17cbd1d99a53adec90dd3ce138410bb"
            };

            // Act
            var serviceResult = await _addressesAppService.CreateAsync(input);

            // Assert
            var result = await _addressRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CIty.ShouldBe("d8626c6");
            result.State.ShouldBe("1f125cb610e941e992ea122a6680bc930ede71b4a54d4a6ca81b166ce0fcfe67cd57c9c996af449e8953787cb5f02");
            result.PostalCode.ShouldBe(7);
            result.Country.ShouldBe(default);
            result.UserId.ShouldBe(Guid.Parse("524ae472-ffbb-4f33-a564-0c4723707594"));
            result.StreetAddress.ShouldBe("7268f9bc3c1e4af49952696d0d1e3a3d191a6c8619f346f79a17cbd1d99a53adec90dd3ce138410bb");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new AddressUpdateDto()
            {
                CIty = "4f931c4d96ef4d52b24d8073e465780a2da3e800d291451",
                State = "94a96b86e26742359b709ece361fdb1363cfa452536c4241bb972431b9e886e30ad7835da04d",
                PostalCode = 6,
                Country = default,
                UserId = Guid.Parse("6ad33807-37f0-4a7a-8237-b3f9ccbc1ab0"),
                StreetAddress = "7127b76d95cc42229654558888ad03bb64ecbe42058c4b998f132ffa5bfd1b9c854b48bbecb7411cb587"
            };

            // Act
            var serviceResult = await _addressesAppService.UpdateAsync(Guid.Parse("96f66c43-719a-4748-837b-c25ef764ba42"), input);

            // Assert
            var result = await _addressRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CIty.ShouldBe("4f931c4d96ef4d52b24d8073e465780a2da3e800d291451");
            result.State.ShouldBe("94a96b86e26742359b709ece361fdb1363cfa452536c4241bb972431b9e886e30ad7835da04d");
            result.PostalCode.ShouldBe(6);
            result.Country.ShouldBe(default);
            result.UserId.ShouldBe(Guid.Parse("6ad33807-37f0-4a7a-8237-b3f9ccbc1ab0"));
            result.StreetAddress.ShouldBe("7127b76d95cc42229654558888ad03bb64ecbe42058c4b998f132ffa5bfd1b9c854b48bbecb7411cb587");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _addressesAppService.DeleteAsync(Guid.Parse("96f66c43-719a-4748-837b-c25ef764ba42"));

            // Assert
            var result = await _addressRepository.FindAsync(c => c.Id == Guid.Parse("96f66c43-719a-4748-837b-c25ef764ba42"));

            result.ShouldBeNull();
        }
    }
}