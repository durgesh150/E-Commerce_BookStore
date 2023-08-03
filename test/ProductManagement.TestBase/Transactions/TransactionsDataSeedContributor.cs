using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ProductManagement.Transactions;

namespace ProductManagement.Transactions
{
    public class TransactionsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public TransactionsDataSeedContributor(ITransactionRepository transactionRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _transactionRepository = transactionRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _transactionRepository.InsertAsync(new Transaction
            (
                id: Guid.Parse("733d4c43-43f2-4989-a454-f7a677d52377"),
                userId: Guid.Parse("0250f38e-af64-4ee4-805f-ff2af04caa8f"),
                transactionDate: new DateTime(2005, 5, 21),
                totalAmount: 1973448307,
                paymentStatus: "d48b38b7381f4478a5e8ae5de68fe506b7c371bf0bed45d08d90fac9ab9d9ea82490962c"
            ));

            await _transactionRepository.InsertAsync(new Transaction
            (
                id: Guid.Parse("de8fefa2-8b57-4c74-8741-1172e87489ee"),
                userId: Guid.Parse("97d1ca1f-f80a-4cc6-83c7-485a1cd62f9e"),
                transactionDate: new DateTime(2021, 6, 22),
                totalAmount: 2074074791,
                paymentStatus: "657b993517ad4b289634ef40dba48da2d143e93203c64014bf66e011f9495e5295cad2e30355473fad4e"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}