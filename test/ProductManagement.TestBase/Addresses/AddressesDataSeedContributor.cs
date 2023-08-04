using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ProductManagement.Addresses;

namespace ProductManagement.Addresses
{
    public class AddressesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IAddressRepository _addressRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public AddressesDataSeedContributor(IAddressRepository addressRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _addressRepository = addressRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _addressRepository.InsertAsync(new Address
            (
                id: Guid.Parse("96f66c43-719a-4748-837b-c25ef764ba42"),
                cIty: "afabe49257f74b64a7bcac3a87b0f1c2366a6c57dcaa437f",
                state: "89d1935df",
                postalCode: 9,
                country: default,
                userId: Guid.Parse("125b1e5a-12f1-49c7-b877-e0f717b81372"),
                streetAddress: "d604660eb7"
            ));

            await _addressRepository.InsertAsync(new Address
            (
                id: Guid.Parse("22d84528-8dfc-4380-ae30-69878eb8eb64"),
                cIty: "55a8d71dbde34bda858539715e4d39418918400b6bfc44e09c77016ada2f45f1b54f2",
                state: "6f017d4ef68c4cddb555e845c84b349334321ac",
                postalCode: 8,
                country: default,
                userId: Guid.Parse("ca1132c6-d278-42d4-bf1e-d73832daab68"),
                streetAddress: "e031b1ced73645e58efa19083e2c"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}