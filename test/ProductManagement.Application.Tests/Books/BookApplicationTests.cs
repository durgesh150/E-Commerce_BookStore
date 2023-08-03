using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ProductManagement.Books
{
    public class BooksAppServiceTests : ProductManagementApplicationTestBase
    {
        private readonly IBooksAppService _booksAppService;
        private readonly IRepository<Book, Guid> _bookRepository;

        public BooksAppServiceTests()
        {
            _booksAppService = GetRequiredService<IBooksAppService>();
            _bookRepository = GetRequiredService<IRepository<Book, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _booksAppService.GetListAsync(new GetBooksInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Book.Id == Guid.Parse("579dc4c9-914b-4e9f-8055-6ad977f48814")).ShouldBe(true);
            result.Items.Any(x => x.Book.Id == Guid.Parse("e1d2e6b4-8cfc-49f1-8e3a-7eb2c70a157e")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _booksAppService.GetAsync(Guid.Parse("579dc4c9-914b-4e9f-8055-6ad977f48814"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("579dc4c9-914b-4e9f-8055-6ad977f48814"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new BookCreateDto
            {
                Title = "145",
                Price = 355639843,
                Description = "7df76b735d1c4dfda54af76886d6f9c9700a284a34dc45268ead978fa9becb09c1b",
                AuthorId = Guid.Parse("a6668e84-e28f-4911-a451-ff88d2905901")
            };

            // Act
            var serviceResult = await _booksAppService.CreateAsync(input);

            // Assert
            var result = await _bookRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Title.ShouldBe("145");
            result.Price.ShouldBe(355639843);
            result.Description.ShouldBe("7df76b735d1c4dfda54af76886d6f9c9700a284a34dc45268ead978fa9becb09c1b");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new BookUpdateDto()
            {
                Title = "1e5",
                Price = 138973102,
                Description = "b9428d992fdf442",
                AuthorId = Guid.Parse("a6668e84-e28f-4911-a451-ff88d2905901")
            };

            // Act
            var serviceResult = await _booksAppService.UpdateAsync(Guid.Parse("579dc4c9-914b-4e9f-8055-6ad977f48814"), input);

            // Assert
            var result = await _bookRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Title.ShouldBe("1e5");
            result.Price.ShouldBe(138973102);
            result.Description.ShouldBe("b9428d992fdf442");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _booksAppService.DeleteAsync(Guid.Parse("579dc4c9-914b-4e9f-8055-6ad977f48814"));

            // Assert
            var result = await _bookRepository.FindAsync(c => c.Id == Guid.Parse("579dc4c9-914b-4e9f-8055-6ad977f48814"));

            result.ShouldBeNull();
        }
    }
}