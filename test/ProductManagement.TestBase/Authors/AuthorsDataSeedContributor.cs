using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ProductManagement.Authors;

namespace ProductManagement.Authors
{
    public class AuthorsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IAuthorRepository _authorRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public AuthorsDataSeedContributor(IAuthorRepository authorRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _authorRepository = authorRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _authorRepository.InsertAsync(new Author
            (
                id: Guid.Parse("a6668e84-e28f-4911-a451-ff88d2905901"),
                firstName: "13",
                lastName: "c79c45f39ce543e7a4f88bfe4ab2248e34f06025f7744740ab01e5",
                email: "fe016f928b54439b87a945864c4cff26a4@93a0a54225e249b9ab7d9fa5a131dcd12b.com",
                bio: "d5efdc6e6ec3445698d66204859528396b2de"
            ));

            await _authorRepository.InsertAsync(new Author
            (
                id: Guid.Parse("d92d289c-5ab7-413d-a6c5-dfaea30d0187"),
                firstName: "db",
                lastName: "e694b53fbc6",
                email: "77fe0d26df52400bb8314e4d4f@b7262accf63f4dbdada02943d7.com",
                bio: "71e2726a74934093bcdccf56f246742dd0ab74ceba18436cac6fed5a21f863"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}