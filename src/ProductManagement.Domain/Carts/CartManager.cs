using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ProductManagement.Carts
{
    public class CartManager : DomainService
    {
        private readonly ICartRepository _cartRepository;

        public CartManager(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<Cart> CreateAsync(
        Guid? bookId, Guid userId, int quantity, DateTime dateAdded)
        {
            Check.NotNull(dateAdded, nameof(dateAdded));

            var cart = new Cart(
             GuidGenerator.Create(),
             bookId, userId, quantity, dateAdded
             );

            return await _cartRepository.InsertAsync(cart);
        }

        public async Task<Cart> UpdateAsync(
            Guid id,
            Guid? bookId, Guid userId, int quantity, DateTime dateAdded, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(dateAdded, nameof(dateAdded));

            var cart = await _cartRepository.GetAsync(id);

            cart.BookId = bookId;
            cart.UserId = userId;
            cart.Quantity = quantity;
            cart.DateAdded = dateAdded;

            cart.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _cartRepository.UpdateAsync(cart);
        }

    }
}