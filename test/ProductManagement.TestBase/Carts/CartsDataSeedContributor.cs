using ProductManagement.Books;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ProductManagement.Carts;

namespace ProductManagement.Carts
{
    public class CartsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly BooksDataSeedContributor _booksDataSeedContributor;

        public CartsDataSeedContributor(ICartRepository cartRepository, IUnitOfWorkManager unitOfWorkManager, BooksDataSeedContributor booksDataSeedContributor)
        {
            _cartRepository = cartRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _booksDataSeedContributor = booksDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _booksDataSeedContributor.SeedAsync(context);

            await _cartRepository.InsertAsync(new Cart
            (
                id: Guid.Parse("c8e464fe-9290-4138-b7dc-5ab17c28e42e"),
                userId: Guid.Parse("58263738-e001-4d79-80af-775b907da915"),
                quantity: 413556234,
                dateAdded: new DateTime(2014, 4, 5),
                bookId: null
            ));

            await _cartRepository.InsertAsync(new Cart
            (
                id: Guid.Parse("fa496b8d-9b1b-4a52-ab9f-4699663b7127"),
                userId: Guid.Parse("e08b1d6b-2610-42af-8614-fdc618a7c463"),
                quantity: 96738614,
                dateAdded: new DateTime(2005, 2, 9),
                bookId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}