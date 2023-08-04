using ProductManagement.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Addresses;

namespace ProductManagement.Web.Pages.Addresses
{
    public class CreateModalModel : ProductManagementPageModel
    {
        [BindProperty]
        public AddressCreateViewModel Address { get; set; }

        private readonly IAddressesAppService _addressesAppService;

        public CreateModalModel(IAddressesAppService addressesAppService)
        {
            _addressesAppService = addressesAppService;

            Address = new();
        }

        public async Task OnGetAsync()
        {
            Address = new AddressCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _addressesAppService.CreateAsync(ObjectMapper.Map<AddressCreateViewModel, AddressCreateDto>(Address));
            return NoContent();
        }
    }

    public class AddressCreateViewModel : AddressCreateDto
    {
    }
}