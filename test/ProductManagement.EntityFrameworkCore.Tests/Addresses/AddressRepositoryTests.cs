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
                    cIty: "82635f6cb16745a6b81bf889b042a76731d4538e12764db7b69abb4532207eab9a425e3d0c8",
                    state: "c9e4c63f69884ad5bc9cc0f94708205a0b53a3dc9bd84",
                    country: default,
                    userId: Guid.Parse("ba184ff2-138b-4c5c-a7cb-abf97cc7def3"),
                    streetAddress: "9e1e6efdce5041189e679da4242c4e1108814318ea07"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("cf3d98a5-9d14-4a34-b6da-a61e5a13e6cf"));
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
                    cIty: "2264a94076614d6683ab47ae95c68f46ee11b0fc0f3f457eb7b7742c7f9f3f8408ed2a7903f8480b",
                    state: "d8a95289d98b46d283e5eda09803abcb01839b672df64558b9baf8620ba99294c2d4825c47044e4dbe17911434f8c77392",
                    country: default,
                    userId: Guid.Parse("b8e3af55-1b9a-4a20-98e9-34a98ffc9caf"),
                    streetAddress: "1df9a29"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}