using ProductManagement.Authors;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ProductManagement.Books;

namespace ProductManagement.Books
{
    public class BooksDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly AuthorsDataSeedContributor _authorsDataSeedContributor;

        public BooksDataSeedContributor(IBookRepository bookRepository, IUnitOfWorkManager unitOfWorkManager, AuthorsDataSeedContributor authorsDataSeedContributor)
        {
            _bookRepository = bookRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _authorsDataSeedContributor = authorsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _authorsDataSeedContributor.SeedAsync(context);

            await _bookRepository.InsertAsync(new Book
            (
                id: Guid.Parse("579dc4c9-914b-4e9f-8055-6ad977f48814"),
                title: "f1a",
                price: 2059405250,
                description: "4b7293f7de834cfca9b300647620fbc1fe",
                authorId: Guid.Parse("a6668e84-e28f-4911-a451-ff88d2905901")
            ));

            await _bookRepository.InsertAsync(new Book
            (
                id: Guid.Parse("e1d2e6b4-8cfc-49f1-8e3a-7eb2c70a157e"),
                title: "b86",
                price: 533927914,
                description: "ccd4d406a0fd4d3d9d",
                authorId: Guid.Parse("a6668e84-e28f-4911-a451-ff88d2905901")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}