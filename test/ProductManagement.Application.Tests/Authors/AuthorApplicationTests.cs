using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ProductManagement.Authors
{
    public class AuthorsAppServiceTests : ProductManagementApplicationTestBase
    {
        private readonly IAuthorsAppService _authorsAppService;
        private readonly IRepository<Author, Guid> _authorRepository;

        public AuthorsAppServiceTests()
        {
            _authorsAppService = GetRequiredService<IAuthorsAppService>();
            _authorRepository = GetRequiredService<IRepository<Author, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _authorsAppService.GetListAsync(new GetAuthorsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("a6668e84-e28f-4911-a451-ff88d2905901")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("d92d289c-5ab7-413d-a6c5-dfaea30d0187")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _authorsAppService.GetAsync(Guid.Parse("a6668e84-e28f-4911-a451-ff88d2905901"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("a6668e84-e28f-4911-a451-ff88d2905901"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new AuthorCreateDto
            {
                FirstName = "bc",
                LastName = "c019d8426a5",
                Email = "537cbc6e8f3c4a00997cc427357f@08b6c058610141e8ad8925da60e2.com",
                Bio = "fb607fd932794694afe69eaf743197da50b01cd66e5341c981e12eae92ac8ce7a9f96069475245aa9f75c773a98c"
            };

            // Act
            var serviceResult = await _authorsAppService.CreateAsync(input);

            // Assert
            var result = await _authorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.FirstName.ShouldBe("bc");
            result.LastName.ShouldBe("c019d8426a5");
            result.Email.ShouldBe("537cbc6e8f3c4a00997cc427357f@08b6c058610141e8ad8925da60e2.com");
            result.Bio.ShouldBe("fb607fd932794694afe69eaf743197da50b01cd66e5341c981e12eae92ac8ce7a9f96069475245aa9f75c773a98c");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new AuthorUpdateDto()
            {
                FirstName = "5c",
                LastName = "39037f1cd10345079620b43f6809996fe51c5a58a6fb4274ae3f24faea6558bd903348ac32714a86a03e7f3fb134eb2",
                Email = "2fb32763f5be4363b@d032416506ab48bcb.com",
                Bio = "3bbed4a1b1114c0f8caba3bbb03c71782c5aabd78b7c474e98e928cedb2e446954b2999f4db8498c9"
            };

            // Act
            var serviceResult = await _authorsAppService.UpdateAsync(Guid.Parse("a6668e84-e28f-4911-a451-ff88d2905901"), input);

            // Assert
            var result = await _authorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.FirstName.ShouldBe("5c");
            result.LastName.ShouldBe("39037f1cd10345079620b43f6809996fe51c5a58a6fb4274ae3f24faea6558bd903348ac32714a86a03e7f3fb134eb2");
            result.Email.ShouldBe("2fb32763f5be4363b@d032416506ab48bcb.com");
            result.Bio.ShouldBe("3bbed4a1b1114c0f8caba3bbb03c71782c5aabd78b7c474e98e928cedb2e446954b2999f4db8498c9");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _authorsAppService.DeleteAsync(Guid.Parse("a6668e84-e28f-4911-a451-ff88d2905901"));

            // Assert
            var result = await _authorRepository.FindAsync(c => c.Id == Guid.Parse("a6668e84-e28f-4911-a451-ff88d2905901"));

            result.ShouldBeNull();
        }
    }
}