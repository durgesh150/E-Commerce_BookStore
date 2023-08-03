using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ProductManagement.Authors;
using ProductManagement.Shared;

namespace ProductManagement.Web.Pages.Authors
{
    public class IndexModel : AbpPageModel
    {
        public string? FirstNameFilter { get; set; }
        public string? LastNameFilter { get; set; }
        public string? EmailFilter { get; set; }
        public string? BioFilter { get; set; }

        private readonly IAuthorsAppService _authorsAppService;

        public IndexModel(IAuthorsAppService authorsAppService)
        {
            _authorsAppService = authorsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}