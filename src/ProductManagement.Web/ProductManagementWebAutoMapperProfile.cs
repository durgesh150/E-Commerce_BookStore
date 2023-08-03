using ProductManagement.Web.Pages.Books;
using ProductManagement.Books;
using ProductManagement.Web.Pages.Authors;
using Volo.Abp.AutoMapper;
using ProductManagement.Authors;
using AutoMapper;

namespace ProductManagement.Web;

public class ProductManagementWebAutoMapperProfile : Profile
{
    public ProductManagementWebAutoMapperProfile()
    {
        //Define your object mappings here, for the Web project

        CreateMap<AuthorDto, AuthorUpdateViewModel>();
        CreateMap<AuthorUpdateViewModel, AuthorUpdateDto>();
        CreateMap<AuthorCreateViewModel, AuthorCreateDto>();

        CreateMap<BookDto, BookUpdateViewModel>();
        CreateMap<BookUpdateViewModel, BookUpdateDto>();
        CreateMap<BookCreateViewModel, BookCreateDto>();
    }
}