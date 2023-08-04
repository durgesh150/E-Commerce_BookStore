using AutoMapper.Internal.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ProductManagement.Addresses;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;
using Volo.Abp.Validation;

namespace ProductManagement.Web.Pages.Addresses
{
    public class AddressModel : PageModel
    {
        private readonly ICurrentUser _currentUser;
        private readonly IAddressesAppService _addressesAppService;
        private readonly ILogger<AddressModel> _logger;
        private readonly AddressManager _addressManager;

        public AddressModel(ICurrentUser currentUser, IAddressesAppService addressesAppService, ILogger<AddressModel> logger , AddressManager addressManager)
        {
            _currentUser = currentUser;
            _addressesAppService = addressesAppService;
            _logger = logger;
            _addressManager = addressManager;
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

        public async Task<IActionResult> OnPost()
        {
            
            if (!ModelState.IsValid)
            {
                // If the form data is not valid, return the same page with validation errors
                return Page();
            }

            if (!Enum.TryParse(typeof(Country), Country.ToString(), out var parsedCountry) || !Enum.IsDefined(typeof(Country), parsedCountry))
            {
                ModelState.AddModelError("", "Invalid Country value.");
                return Page();
            }
            // Save the address details to the database or perform any other required actions
            var address = new AddressCreateDto
            {
                UserId = (Guid)_currentUser.Id,
                StreetAddress = StreetAddress,
                CIty = City,
                State = State,
                PostalCode = PostalCode,
                Country = (Country)Enum.Parse(typeof(Country), Country.ToString())
            };

            try
            {

                var createdAddress = await _addressesAppService.CreateAsync(address);

                return RedirectToPage("/Address");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while creating the address. Please try again later.");
                _logger.LogError(ex, "An error occurred while creating the address.");
                return Page();
            }
        }


    }
}
