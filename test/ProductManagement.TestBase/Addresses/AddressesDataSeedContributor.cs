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
                id: Guid.Parse("e27c9730-4840-480c-8901-4064b5062be0"),
                cIty: "02fc3d209ead48c3909140da8ff4abacecfc3d57829f4597b1f0788e43ab9e255b1b250c96364ccd91e",
                state: "121c41379ae24838ad29fc20dd209a8092f65b6a57bd4f6d871fe06ef6db6653",
                postalCode: 7,
                country: default,
                userId: Guid.Parse("586711e2-9f24-4f29-a830-9e29006a00c3")
            ));

            await _addressRepository.InsertAsync(new Address
            (
                id: Guid.Parse("2eed3cd1-c4e2-41fe-adc3-a557a7b13af7"),
                cIty: "3a861f053fff4da2939c944a70dbe3b6f834bf65d1944d149ae3cb74da9",
                state: "3df7c18fcb4449",
                postalCode: 8,
                country: default,
                userId: Guid.Parse("7b935bdf-9528-4dad-aec1-a5536140f725")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}