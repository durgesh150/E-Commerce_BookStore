using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Books;
using ProductManagement.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Users;
using ProductManagement.Web.ViewModels;
using static Volo.Abp.Identity.Web.Pages.Identity.Users.Lock;


namespace ProductManagement.Web.Pages
{
    public class IndexModel : ProductManagementPageModel
    {
        private readonly IBooksAppService _booksAppService;
        private readonly NavigationManager _navigationManager;
        private readonly ICurrentUser _currentUser;
        private readonly ICartsAppService _cartsAppService;

        public IndexModel(IBooksAppService booksAppService, NavigationManager navigationManager, ICurrentUser currentUser, ICartsAppService cartsAppService)
        {
            _booksAppService = booksAppService;
            _navigationManager = navigationManager;
            _currentUser = currentUser;
            _cartsAppService = cartsAppService;

            Books = new List<BookViewModel>(); 
        }

        public List<BookViewModel> Books { get; set; } 

        GetBooksInput input = new GetBooksInput();

        public async Task OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                var redirectUrl = UriHelper.BuildAbsolute(HttpContext.Request.Scheme, HttpContext.Request.Host, "/Account/Login");
                await HttpContext.ChallengeAsync(new AuthenticationProperties
                {
                    RedirectUri = redirectUrl
                });
                return;
            }

            // Fetch book titles, author names, and prices from the service
            var result = await _booksAppService.GetListAsync(input);

            if (result.Items != null)
            {
                // Populate the Books list
                Books = result.Items.Select(b => new BookViewModel
                {
                    Id = b.Book.Id,
                    Title = b.Book.Title,
                    AuthorName = b.Author.FirstName,
                    Price = b.Book.Price
                }).ToList();
            }

            // Fetch cart quantities for each book category and update the CartQuantity property within the Books list
            Guid currentUserId = _currentUser.Id ?? Guid.Empty;
            var cartItems = await _cartsAppService.GetByUserId(currentUserId);
            foreach (var cartItem in cartItems)
            {
                Guid bookGuid = cartItem.BookId;
                var book = Books.FirstOrDefault(b => b.Id == bookGuid);
                if (book != null)
                {
                    book.CartQuantity = cartItem.Quantity;
                }
            }
        }

        public async Task<IActionResult> OnPostAddToCart(Guid Id)
        {
            var currentUserId = _currentUser.Id ?? Guid.Empty;
            var book = Books.FirstOrDefault(b => b.Id == Id);
            if (book != null)
            {
                book.CartQuantity++;
            }

            // Prepare the CartCreateDto with relevant data
            var cartCreateDto = new CartCreateDto
            {
                UserId = currentUserId,
                Quantity = 1, // You can set the initial quantity here if needed
                DateAdded = DateTime.UtcNow,
                BookId = Id
            };

            // Call the cart service method to add the item to the cart
            await _cartsAppService.CreateAsync(cartCreateDto);

            // Optionally, you can show a success message indicating the item was added to the cart
            TempData["CartMessage"] = "Item added to cart.";

            // Redirect back to the same page (refresh) to update the cart content
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveFromCart(Guid bookId)
        {
            var currentUserId = _currentUser.Id ?? Guid.Empty;

            // Find the cart item for the specified book and user
            var cartItem = (await _cartsAppService.GetByUserId(currentUserId))
                .FirstOrDefault(item => item.BookId == bookId);

            if (cartItem != null)
            {
                // Delete the cart item from the cart
                await _cartsAppService.DeleteAsync(cartItem.Id);

                // Update the CartQuantity property for the removed book
                var book = Books.FirstOrDefault(b => b.Id == bookId);
                if (book != null)
                {
                    book.CartQuantity = 0;
                }
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateQuantity(Guid bookId, int quantity)
        {
            var currentUserId = _currentUser.Id ?? Guid.Empty;

            // Find the cart item for the specified book and user
            var cartItem = (await _cartsAppService.GetByUserId(currentUserId))
                .FirstOrDefault(item => item.BookId == bookId);

            if (cartItem != null)
            {
                CartUpdateDto cartUpdateDto = new CartUpdateDto
                {
                    BookId = bookId,
                    DateAdded = cartItem.DateAdded,
                    UserId = cartItem.UserId,
                    Quantity = quantity,
                    ConcurrencyStamp = cartItem.ConcurrencyStamp
                };

                // Call the cart service method to update the cart item
                await _cartsAppService.UpdateAsync(cartItem.Id, cartUpdateDto);

            }

            return RedirectToPage();
        }
    }
}
