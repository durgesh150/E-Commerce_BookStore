using ProductManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ProductManagement.Addresses;
using ProductManagement.Shared;

namespace ProductManagement.Web.Pages.Addresses
{
    public class IndexModel : AbpPageModel
    {
        public string? CItyFilter { get; set; }
        public string? StateFilter { get; set; }
        public long? PostalCodeFilterMin { get; set; }

        public long? PostalCodeFilterMax { get; set; }
        public Country? CountryFilter { get; set; }
        public string? UserIdFilter { get; set; }
        public string? StreetAddressFilter { get; set; }

        private readonly IAddressesAppService _addressesAppService;

        public IndexModel(IAddressesAppService addressesAppService)
        {
            _addressesAppService = addressesAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}