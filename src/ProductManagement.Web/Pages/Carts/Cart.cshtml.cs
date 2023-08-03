using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductManagement.Authors;
using ProductManagement.Books;
using ProductManagement.Carts;
using Volo.Abp.Users;

namespace ProductManagement.Web.Pages.Carts;

public class CartModel : PageModel
{
    private readonly ICurrentUser _currentUser;
    private readonly ICartsAppService _cartsAppService;
    private readonly IBooksAppService _booksAppService;
    private readonly IAuthorsAppService _authorsAppService; 

    public CartModel(ICurrentUser currentUser, ICartsAppService cartsAppService, IBooksAppService booksAppService, IAuthorsAppService authorsAppService) // Add IAuthorsAppService parameter
    {
        _currentUser = currentUser;
        _cartsAppService = cartsAppService;
        _booksAppService = booksAppService;
        _authorsAppService = authorsAppService;
    }

    public List<CartDto> CartItems { get; set; }
   
    public List<string> AuthorNames { get; set; }
    public List<float> Prices { get; set; }
    public List<Guid> Id { get; set; }
    public Dictionary<Guid, CartDto> UniqueCartItems { get; set; }
    public Dictionary<Guid, string> BookTitles { get; set; }
    public Dictionary<Guid,float> Price { get; set; }

    public async Task OnGet()
    {
        Guid currentUserId = _currentUser.Id ?? Guid.Empty;

        // Retrieve cart items for the current user from the service
        CartItems = await _cartsAppService.GetByUserId(currentUserId);

        // Populate unique cart items in the dictionary
        UniqueCartItems = new Dictionary<Guid, CartDto>();
        foreach (var item in CartItems)
        {
            Guid bookguid = item.BookId;
            // If the book is already in the dictionary, update its quantity
            if (UniqueCartItems.ContainsKey(bookguid))
            {
                UniqueCartItems[bookguid].Quantity += item.Quantity;
            }
            else // If the book is not already in the dictionary, add it as a new entry
            {
                UniqueCartItems.Add(bookguid, item);
            }
        }

        // Fetch book titles separately for each unique cart item
        BookTitles = new Dictionary<Guid, string>();
        Price = new Dictionary<Guid, float>();
        foreach (var uniqueCartItem in UniqueCartItems)
        {
            var book = await _booksAppService.GetAsync(uniqueCartItem.Key);
            if (book != null)
            {
                BookTitles.Add(uniqueCartItem.Key, book.Title);
                Price.Add(uniqueCartItem.Key, book.Price);
            }
        }
    }


    public async Task<IActionResult> OnPostRemoveFromCart(Guid Id)
    {
        // Get the current user ID from the authentication system
        Guid currentUserId = _currentUser.Id ?? Guid.Empty;
        await _cartsAppService.DeleteAsync(Id);
        // Optionally, you can show a success message indicating the item was removed from the cart
        TempData["CartMessage"] = "Item removed from cart.";

        // Redirect back to the same page (refresh) to update the cart content
        return RedirectToPage();
    }
}
