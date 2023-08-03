using ProductManagement.Carts;
using ProductManagement.TransactionDetails;
using ProductManagement.Transactions;
using ProductManagement.Addresses;
using ProductManagement.Books;
using System;
using ProductManagement.Shared;
using Volo.Abp.AutoMapper;
using ProductManagement.Authors;
using AutoMapper;

namespace ProductManagement;

public class ProductManagementApplicationAutoMapperProfile : Profile
{
    public ProductManagementApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Author, AuthorDto>();
        CreateMap<Author, AuthorExcelDto>();

        CreateMap<Book, BookDto>();
        CreateMap<Book, BookExcelDto>();
        CreateMap<BookWithNavigationProperties, BookWithNavigationPropertiesDto>();
        CreateMap<Author, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.FirstName));

        CreateMap<Address, AddressDto>();

        CreateMap<Transaction, TransactionDto>();

        CreateMap<TransactionDetail, TransactionDetailDto>();
        CreateMap<TransactionDetailWithNavigationProperties, TransactionDetailWithNavigationPropertiesDto>();
        CreateMap<Transaction, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.PaymentStatus));
        CreateMap<Book, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Title));

        CreateMap<Cart, CartDto>();
        CreateMap<CartWithNavigationProperties, CartWithNavigationPropertiesDto>();
    }
}