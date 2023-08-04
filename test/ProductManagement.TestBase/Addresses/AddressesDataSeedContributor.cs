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
                id: Guid.Parse("cf3d98a5-9d14-4a34-b6da-a61e5a13e6cf"),
                cIty: "82635f6cb16745a6b81bf889b042a76731d4538e12764db7b69abb4532207eab9a425e3d0c8",
                state: "c9e4c63f69884ad5bc9cc0f94708205a0b53a3dc9bd84",
                postalCode: 797180244,
                country: default,
                userId: Guid.Parse("ba184ff2-138b-4c5c-a7cb-abf97cc7def3"),
                streetAddress: "9e1e6efdce5041189e679da4242c4e1108814318ea07"
            ));

            await _addressRepository.InsertAsync(new Address
            (
                id: Guid.Parse("27c624ac-e76d-43f5-97cb-2403e6ca0961"),
                cIty: "2264a94076614d6683ab47ae95c68f46ee11b0fc0f3f457eb7b7742c7f9f3f8408ed2a7903f8480b",
                state: "d8a95289d98b46d283e5eda09803abcb01839b672df64558b9baf8620ba99294c2d4825c47044e4dbe17911434f8c77392",
                postalCode: 734139928,
                country: default,
                userId: Guid.Parse("b8e3af55-1b9a-4a20-98e9-34a98ffc9caf"),
                streetAddress: "1df9a29"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}