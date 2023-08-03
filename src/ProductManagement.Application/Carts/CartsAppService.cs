using ProductManagement.Shared;
using ProductManagement.Books;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using ProductManagement.Permissions;
using ProductManagement.Carts;
using Volo.Abp.ObjectMapping;

namespace ProductManagement.Carts
{

    [Authorize(ProductManagementPermissions.Carts.Default)]
    public class CartsAppService : ApplicationService, ICartsAppService
    {

        private readonly ICartRepository _cartRepository;
        private readonly CartManager _cartManager;
        private readonly IRepository<Book, Guid> _bookRepository;

        public CartsAppService(ICartRepository cartRepository, CartManager cartManager, IRepository<Book, Guid> bookRepository)
        {

            _cartRepository = cartRepository;
            _cartManager = cartManager; _bookRepository = bookRepository;
        }

        public virtual async Task<PagedResultDto<CartWithNavigationPropertiesDto>> GetListAsync(GetCartsInput input)
        {
            var totalCount = await _cartRepository.GetCountAsync(input.FilterText, input.UserId, input.QuantityMin, input.QuantityMax, input.DateAddedMin, input.DateAddedMax, input.BookId);
            var items = await _cartRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.UserId, input.QuantityMin, input.QuantityMax, input.DateAddedMin, input.DateAddedMax, input.BookId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CartWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CartWithNavigationProperties>, List<CartWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CartWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CartWithNavigationProperties, CartWithNavigationPropertiesDto>
                (await _cartRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<CartDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Cart, CartDto>(await _cartRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetBookLookupAsync(LookupRequestDto input)
        {
            var query = (await _bookRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Title != null &&
                         x.Title.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Book>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Book>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(ProductManagementPermissions.Carts.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _cartRepository.DeleteAsync(id);
        }

        [Authorize(ProductManagementPermissions.Carts.Create)]
        public virtual async Task<CartDto> CreateAsync(CartCreateDto input)
        {

            var cart = await _cartManager.CreateAsync(
            input.BookId, input.UserId, input.Quantity, input.DateAdded
            );

            return ObjectMapper.Map<Cart, CartDto>(cart);
        }

        [Authorize(ProductManagementPermissions.Carts.Edit)]
        public virtual async Task<CartDto> UpdateAsync(Guid id, CartUpdateDto input)
        {

            var cart = await _cartManager.UpdateAsync(
            id,
            input.BookId, input.UserId, input.Quantity, input.DateAdded, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Cart, CartDto>(cart);
        }
        [Authorize(ProductManagementPermissions.Carts.Default)]
        public virtual async Task<List<CartDto>> GetByUserId(Guid? userid)
        {
          var query = (await _cartRepository.GetQueryableAsync())
                .WhereIf(userid != null,
                                   x => x.UserId == userid);
            var cart =  query.ToList();
            return ObjectMapper.Map<List<Cart>,List<CartDto>>(cart);
        }
    }
}