using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ProductManagement.Authors
{
    public class AuthorManager : DomainService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorManager(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> CreateAsync(
        string firstName, string lastName, string email, string bio)
        {
            Check.NotNullOrWhiteSpace(firstName, nameof(firstName));
            Check.Length(firstName, nameof(firstName), int.MaxValue, AuthorConsts.FirstNameMinLength);
            Check.NotNullOrWhiteSpace(email, nameof(email));

            var author = new Author(
             GuidGenerator.Create(),
             firstName, lastName, email, bio
             );

            return await _authorRepository.InsertAsync(author);
        }

        public async Task<Author> UpdateAsync(
            Guid id,
            string firstName, string lastName, string email, string bio, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(firstName, nameof(firstName));
            Check.Length(firstName, nameof(firstName), int.MaxValue, AuthorConsts.FirstNameMinLength);
            Check.NotNullOrWhiteSpace(email, nameof(email));

            var author = await _authorRepository.GetAsync(id);

            author.FirstName = firstName;
            author.LastName = lastName;
            author.Email = email;
            author.Bio = bio;

            author.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _authorRepository.UpdateAsync(author);
        }

    }
}