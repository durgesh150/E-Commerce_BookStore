using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProductManagement.Books;
using ProductManagement.Carts;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Users;
using Volo.Payment.Requests;


namespace ProductManagement.Web.Payments
{
    public class IndexModel : PageModel
    {
        public string SessionId { get; set; }
        private readonly IPaymentRequestAppService _paymentRequestAppService;
        private readonly ICurrentUser _currentUser;
        private readonly ICartsAppService _cartsAppService;
        private readonly IBooksAppService _booksAppService;
        private readonly ICartRepository _cartRepository;

        public List<Cart> CartItems { get; set; }

        public List<string> AuthorNames { get; set; }
        public List<float> Prices { get; set; }
        public List<Guid> Id { get; set; }
        public Dictionary<Guid, Cart> UniqueCartItems { get; set; }
        public Dictionary<Guid, string> BookTitles { get; set; }
        public Dictionary<Guid, float> Price { get; set; }

        public IndexModel(IPaymentRequestAppService paymentRequestAppService, ICurrentUser currentUser, ICartsAppService cartsAppService, IBooksAppService booksAppService, ICartRepository cartRepository)
        {
            _paymentRequestAppService = paymentRequestAppService;
            _currentUser = currentUser;
            _cartsAppService = cartsAppService;
            _booksAppService = booksAppService;
            _cartRepository = cartRepository;
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            Guid currentUserId = _currentUser.Id ?? Guid.Empty;

            CartItems = await _cartRepository.GetListAsync();
            CartItems = CartItems.Where(cartItem => cartItem.UserId == currentUserId).ToList();

            // Populate unique cart items in the dictionary
            UniqueCartItems = new Dictionary<Guid, Cart>();
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
            var domain = "https://localhost:44389/";

            var lineItems = new List<SessionLineItemOptions>();
            foreach (var uniqueCartItem in UniqueCartItems.Values)
            {
                if (BookTitles.ContainsKey(uniqueCartItem.BookId))
                {
                    var itemName = BookTitles[uniqueCartItem.BookId];
                    var itemPrice = Price[uniqueCartItem.BookId] * uniqueCartItem.Quantity * 100;

                    lineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmountDecimal = (long)itemPrice,
                            Currency = "inr",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = itemName,
                            },
                        },
                        Quantity = 1,
                    });
                }
            }

            var totalAmount = (decimal)UniqueCartItems.Values.Sum(item => Price[item.BookId] * item.Quantity);

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
        {
            "card",
        },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = domain + "payments/Success",
                CancelUrl = domain + "Carts/cart",
            };
            var service = new SessionService();
            Session session = service.Create(options);
            Response.Headers.Add("Location", session.Url);

            return Redirect(session.Url);
        }


        public async void OnGet()
        {
            Guid currentUserId = _currentUser.Id ?? Guid.Empty;


            CartItems = await _cartRepository.GetListAsync();
            CartItems = CartItems.Where(cartItem => cartItem.UserId == currentUserId).ToList();

            // Populate unique cart items in the dictionary
            UniqueCartItems = new Dictionary<Guid, Cart>();
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

            decimal totalAmount = (decimal)UniqueCartItems.Values.Sum(item => Price[item.BookId] * item.Quantity);
            ViewData["TotalAmount"] = totalAmount;
        }


    }


}
