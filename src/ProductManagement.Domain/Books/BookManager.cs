using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ProductManagement.Books
{
    public class BookManager : DomainService
    {
        private readonly IBookRepository _bookRepository;

        public BookManager(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> CreateAsync(
        Guid authorId, string title, float price, string description)
        {
            Check.NotNull(authorId, nameof(authorId));
            Check.NotNullOrWhiteSpace(title, nameof(title));
            Check.Length(title, nameof(title), int.MaxValue, BookConsts.TitleMinLength);

            var book = new Book(
             GuidGenerator.Create(),
             authorId, title, price, description
             );

            return await _bookRepository.InsertAsync(book);
        }

        public async Task<Book> UpdateAsync(
            Guid id,
            Guid authorId, string title, float price, string description, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(authorId, nameof(authorId));
            Check.NotNullOrWhiteSpace(title, nameof(title));
            Check.Length(title, nameof(title), int.MaxValue, BookConsts.TitleMinLength);

            var book = await _bookRepository.GetAsync(id);

            book.AuthorId = authorId;
            book.Title = title;
            book.Price = price;
            book.Description = description;

            book.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _bookRepository.UpdateAsync(book);
        }

    }
}