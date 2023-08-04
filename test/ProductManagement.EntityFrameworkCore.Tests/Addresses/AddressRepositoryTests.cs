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
                    cIty: "afabe49257f74b64a7bcac3a87b0f1c2366a6c57dcaa437f",
                    state: "89d1935df",
                    country: default,
                    userId: Guid.Parse("125b1e5a-12f1-49c7-b877-e0f717b81372"),
                    streetAddress: "d604660eb7"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("96f66c43-719a-4748-837b-c25ef764ba42"));
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
                    cIty: "55a8d71dbde34bda858539715e4d39418918400b6bfc44e09c77016ada2f45f1b54f2",
                    state: "6f017d4ef68c4cddb555e845c84b349334321ac",
                    country: default,
                    userId: Guid.Parse("ca1132c6-d278-42d4-bf1e-d73832daab68"),
                    streetAddress: "e031b1ced73645e58efa19083e2c"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}