using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ProductManagement.Books;
using ProductManagement.EntityFrameworkCore;
using Xunit;

namespace ProductManagement.Books
{
    public class BookRepositoryTests : ProductManagementEntityFrameworkCoreTestBase
    {
        private readonly IBookRepository _bookRepository;

        public BookRepositoryTests()
        {
            _bookRepository = GetRequiredService<IBookRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _bookRepository.GetListAsync(
                    title: "f1a",
                    description: "4b7293f7de834cfca9b300647620fbc1fe"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("579dc4c9-914b-4e9f-8055-6ad977f48814"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _bookRepository.GetCountAsync(
                    title: "b86",
                    description: "ccd4d406a0fd4d3d9d"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}