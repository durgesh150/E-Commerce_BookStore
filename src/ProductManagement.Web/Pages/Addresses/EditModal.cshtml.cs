using ProductManagement.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ProductManagement.Addresses;

namespace ProductManagement.Web.Pages.Addresses
{
    public class EditModalModel : ProductManagementPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public AddressUpdateViewModel Address { get; set; }

        private readonly IAddressesAppService _addressesAppService;

        public EditModalModel(IAddressesAppService addressesAppService)
        {
            _addressesAppService = addressesAppService;

            Address = new();
        }

        public async Task OnGetAsync()
        {
            var address = await _addressesAppService.GetAsync(Id);
            Address = ObjectMapper.Map<AddressDto, AddressUpdateViewModel>(address);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _addressesAppService.UpdateAsync(Id, ObjectMapper.Map<AddressUpdateViewModel, AddressUpdateDto>(Address));
            return NoContent();
        }
    }

    public class AddressUpdateViewModel : AddressUpdateDto
    {
    }
}