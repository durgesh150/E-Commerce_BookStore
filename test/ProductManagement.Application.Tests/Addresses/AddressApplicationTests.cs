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
            result.Items.Any(x => x.Id == Guid.Parse("e27c9730-4840-480c-8901-4064b5062be0")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("2eed3cd1-c4e2-41fe-adc3-a557a7b13af7")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _addressesAppService.GetAsync(Guid.Parse("e27c9730-4840-480c-8901-4064b5062be0"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("e27c9730-4840-480c-8901-4064b5062be0"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new AddressCreateDto
            {
                CIty = "1f207ddb40c24ec4a21c93520fae4901646326a289f944f79ac458bc6a45b0378208258dbf1c4d02869398330faa2392",
                State = "4d42425b8a4c41938ed588349828f83dbaacb1a7247f4e4799d083c9d003f9c476ce8a38a89b4d619d19f99fa3a041c3",
                PostalCode = 6,
                Country = default,
                UserId = Guid.Parse("6872cee6-4ace-4f3d-afc8-cdafddcc4d35")
            };

            // Act
            var serviceResult = await _addressesAppService.CreateAsync(input);

            // Assert
            var result = await _addressRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CIty.ShouldBe("1f207ddb40c24ec4a21c93520fae4901646326a289f944f79ac458bc6a45b0378208258dbf1c4d02869398330faa2392");
            result.State.ShouldBe("4d42425b8a4c41938ed588349828f83dbaacb1a7247f4e4799d083c9d003f9c476ce8a38a89b4d619d19f99fa3a041c3");
            result.PostalCode.ShouldBe(6);
            result.Country.ShouldBe(default);
            result.UserId.ShouldBe(Guid.Parse("6872cee6-4ace-4f3d-afc8-cdafddcc4d35"));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new AddressUpdateDto()
            {
                CIty = "1b18c7760b3a4d49b717da576c7362373d50ad899e9347089f09803d505b25bafada846c6e244",
                State = "4ba61d1bd149420d83981c5d123d5b4f0625b39fe3b649a3aaa2d157cb8fe67392737ba04ad34d3d8bbaf33",
                PostalCode = 7,
                Country = default,
                UserId = Guid.Parse("2788895f-22c4-4f3f-85cb-38c830aec160")
            };

            // Act
            var serviceResult = await _addressesAppService.UpdateAsync(Guid.Parse("e27c9730-4840-480c-8901-4064b5062be0"), input);

            // Assert
            var result = await _addressRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CIty.ShouldBe("1b18c7760b3a4d49b717da576c7362373d50ad899e9347089f09803d505b25bafada846c6e244");
            result.State.ShouldBe("4ba61d1bd149420d83981c5d123d5b4f0625b39fe3b649a3aaa2d157cb8fe67392737ba04ad34d3d8bbaf33");
            result.PostalCode.ShouldBe(7);
            result.Country.ShouldBe(default);
            result.UserId.ShouldBe(Guid.Parse("2788895f-22c4-4f3f-85cb-38c830aec160"));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _addressesAppService.DeleteAsync(Guid.Parse("e27c9730-4840-480c-8901-4064b5062be0"));

            // Assert
            var result = await _addressRepository.FindAsync(c => c.Id == Guid.Parse("e27c9730-4840-480c-8901-4064b5062be0"));

            result.ShouldBeNull();
        }
    }
}