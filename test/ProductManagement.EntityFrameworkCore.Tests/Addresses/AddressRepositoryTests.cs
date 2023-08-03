using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ProductManagement.Addresses;
using ProductManagement.EntityFrameworkCore;
using Xunit;

namespace ProductManagement.Addresses
{
    public class AddressRepositoryTests : ProductManagementEntityFrameworkCoreTestBase
    {
        private readonly IAddressRepository _addressRepository;

        public AddressRepositoryTests()
        {
            _addressRepository = GetRequiredService<IAddressRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _addressRepository.GetListAsync(
                    cIty: "02fc3d209ead48c3909140da8ff4abacecfc3d57829f4597b1f0788e43ab9e255b1b250c96364ccd91e",
                    state: "121c41379ae24838ad29fc20dd209a8092f65b6a57bd4f6d871fe06ef6db6653",
                    country: default,
                    userId: Guid.Parse("586711e2-9f24-4f29-a830-9e29006a00c3")
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("e27c9730-4840-480c-8901-4064b5062be0"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _addressRepository.GetCountAsync(
                    cIty: "3a861f053fff4da2939c944a70dbe3b6f834bf65d1944d149ae3cb74da9",
                    state: "3df7c18fcb4449",
                    country: default,
                    userId: Guid.Parse("7b935bdf-9528-4dad-aec1-a5536140f725")
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}