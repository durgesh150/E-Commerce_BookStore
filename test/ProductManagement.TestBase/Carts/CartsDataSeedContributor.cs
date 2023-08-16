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
                id: Guid.Parse("120b15bf-0943-46cd-8906-c0624cb39a4c"),
                userId: Guid.Parse("3242797a-2978-4259-a65b-a99b0669b19f"),
                quantity: 1816891000,
                dateAdded: new DateTime(2004, 8, 4),
                unitPrice: 1314264770,
                totalPrice: 1772079697,
                lastModified: new DateTime(2015, 10, 19),
                bookId: Guid.Parse("579dc4c9-914b-4e9f-8055-6ad977f48814")
            ));

            await _cartRepository.InsertAsync(new Cart
            (
                id: Guid.Parse("9db0d161-312e-4c71-a051-479d14d30c13"),
                userId: Guid.Parse("b8c7509f-7a95-4ab5-a0be-00c36118da3a"),
                quantity: 2082293026,
                dateAdded: new DateTime(2011, 4, 1),
                unitPrice: 1791550101,
                totalPrice: 1612670648,
                lastModified: new DateTime(2005, 1, 7),
                bookId: Guid.Parse("579dc4c9-914b-4e9f-8055-6ad977f48814")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}