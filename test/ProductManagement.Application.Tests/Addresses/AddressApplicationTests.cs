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
            result.Items.Any(x => x.Id == Guid.Parse("cf3d98a5-9d14-4a34-b6da-a61e5a13e6cf")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("27c624ac-e76d-43f5-97cb-2403e6ca0961")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _addressesAppService.GetAsync(Guid.Parse("cf3d98a5-9d14-4a34-b6da-a61e5a13e6cf"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("cf3d98a5-9d14-4a34-b6da-a61e5a13e6cf"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new AddressCreateDto
            {
                CIty = "afcc6aa1c7694441a0d682ca34802e29e",
                State = "347537fdb73a4d6d8c0cf9",
                PostalCode = 799743243,
                Country = default,
                UserId = Guid.Parse("131adc6e-2c1c-4ad8-8eca-0b869cfd8895"),
                StreetAddress = "ca519aaf2a794ec686e70b1c01ad241363d7a66b946144ad8238618c2f9630f0065dcd1f57044e"
            };

            // Act
            var serviceResult = await _addressesAppService.CreateAsync(input);

            // Assert
            var result = await _addressRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CIty.ShouldBe("afcc6aa1c7694441a0d682ca34802e29e");
            result.State.ShouldBe("347537fdb73a4d6d8c0cf9");
            result.PostalCode.ShouldBe(799743243);
            result.Country.ShouldBe(default);
            result.UserId.ShouldBe(Guid.Parse("131adc6e-2c1c-4ad8-8eca-0b869cfd8895"));
            result.StreetAddress.ShouldBe("ca519aaf2a794ec686e70b1c01ad241363d7a66b946144ad8238618c2f9630f0065dcd1f57044e");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new AddressUpdateDto()
            {
                CIty = "0c865d56198244e1bc",
                State = "cb82135c7e62416cb4d7792b0c28c3b03e9ddf7f3cf241dab8f237e147e8b",
                PostalCode = 1480786873,
                Country = default,
                UserId = Guid.Parse("321fc28b-deb2-4787-8c5f-6ec50f2428ca"),
                StreetAddress = "f1949a9e7c814656aad0cf345de454829805bdb3baa5461a"
            };

            // Act
            var serviceResult = await _addressesAppService.UpdateAsync(Guid.Parse("cf3d98a5-9d14-4a34-b6da-a61e5a13e6cf"), input);

            // Assert
            var result = await _addressRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CIty.ShouldBe("0c865d56198244e1bc");
            result.State.ShouldBe("cb82135c7e62416cb4d7792b0c28c3b03e9ddf7f3cf241dab8f237e147e8b");
            result.PostalCode.ShouldBe(1480786873);
            result.Country.ShouldBe(default);
            result.UserId.ShouldBe(Guid.Parse("321fc28b-deb2-4787-8c5f-6ec50f2428ca"));
            result.StreetAddress.ShouldBe("f1949a9e7c814656aad0cf345de454829805bdb3baa5461a");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _addressesAppService.DeleteAsync(Guid.Parse("cf3d98a5-9d14-4a34-b6da-a61e5a13e6cf"));

            // Assert
            var result = await _addressRepository.FindAsync(c => c.Id == Guid.Parse("cf3d98a5-9d14-4a34-b6da-a61e5a13e6cf"));

            result.ShouldBeNull();
        }
    }
}