using ProductManagement.Books;
using ProductManagement.Transactions;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ProductManagement.TransactionDetails;

namespace ProductManagement.TransactionDetails
{
    public class TransactionDetailsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ITransactionDetailRepository _transactionDetailRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly TransactionsDataSeedContributor _transactionsDataSeedContributor;

        private readonly BooksDataSeedContributor _booksDataSeedContributor;

        public TransactionDetailsDataSeedContributor(ITransactionDetailRepository transactionDetailRepository, IUnitOfWorkManager unitOfWorkManager, TransactionsDataSeedContributor transactionsDataSeedContributor, BooksDataSeedContributor booksDataSeedContributor)
        {
            _transactionDetailRepository = transactionDetailRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _transactionsDataSeedContributor = transactionsDataSeedContributor; _booksDataSeedContributor = booksDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _transactionsDataSeedContributor.SeedAsync(context);
            await _booksDataSeedContributor.SeedAsync(context);

            await _transactionDetailRepository.InsertAsync(new TransactionDetail
            (
                id: Guid.Parse("1fb3f283-5af1-4729-8e07-ecf3a4e5ec8d"),
                quantity: 952145457,
                subTotal: 1737398611,
                transactionId: null,
                bookId: null
            ));

            await _transactionDetailRepository.InsertAsync(new TransactionDetail
            (
                id: Guid.Parse("e4214abd-032d-46cb-ad74-d12777bcf4b2"),
                quantity: 380823595,
                subTotal: 523972998,
                transactionId: null,
                bookId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}