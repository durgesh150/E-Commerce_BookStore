using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ProductManagement.Authors;
using ProductManagement.EntityFrameworkCore;
using Xunit;

namespace ProductManagement.Authors
{
    public class AuthorRepositoryTests : ProductManagementEntityFrameworkCoreTestBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorRepositoryTests()
        {
            _authorRepository = GetRequiredService<IAuthorRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _authorRepository.GetListAsync(
                    firstName: "13",
                    lastName: "c79c45f39ce543e7a4f88bfe4ab2248e34f06025f7744740ab01e5",
                    email: "fe016f928b54439b87a945864c4cff26a4@93a0a54225e249b9ab7d9fa5a131dcd12b.com",
                    bio: "d5efdc6e6ec3445698d66204859528396b2de"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("a6668e84-e28f-4911-a451-ff88d2905901"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _authorRepository.GetCountAsync(
                    firstName: "db",
                    lastName: "e694b53fbc6",
                    email: "77fe0d26df52400bb8314e4d4f@b7262accf63f4dbdada02943d7.com",
                    bio: "71e2726a74934093bcdccf56f246742dd0ab74ceba18436cac6fed5a21f863"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}