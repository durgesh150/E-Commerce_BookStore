using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductManagement.Addresses;
using System;
using Volo.Abp.Users;

namespace ProductManagement.Web.Pages.Addresses
{
    public class AddressModel : PageModel
    {
        private readonly ICurrentUser _currentUser;
        private readonly IAddressesAppService _addressesAppService;

        public AddressModel(ICurrentUser currentUser, IAddressesAppService addressesAppService)
        {
            _currentUser = currentUser;
            _addressesAppService = addressesAppService;
        }

        [BindProperty]
        public string StreetAddress { get; set; }

        [BindProperty]
        public string City { get; set; }

        [BindProperty]
        public string State { get; set; }

        [BindProperty]
        public long PostalCode { get; set; }

        [BindProperty]
        public Country Country { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // If the form data is not valid, return the same page with validation errors
                return Page();
            }

            // Convert the string value back to the Country enum type
            if (!Enum.TryParse(typeof(Country), Country.ToString(), out var countryEnum))
            {
                countryEnum = Country.Others;
            }

            // Save the address details to the database or perform any other required actions
            var address = new AddressCreateDto
            {
                StreetAddress = StreetAddress,
                CIty = City,
                State = State,
                PostalCode = PostalCode,
                Country = (Country)countryEnum // Cast the enum value back to Country
            };

            // Code for saving the address to the database goes here...
             await _addressesAppService.CreateAsync(address);

            // Optionally, you can show a success message to the user
            TempData["AddressMessage"] = "Address saved successfully.";

            // Redirect the user to a different page after successful submission
            return RedirectToPage("/Review");
        }

    }
}
