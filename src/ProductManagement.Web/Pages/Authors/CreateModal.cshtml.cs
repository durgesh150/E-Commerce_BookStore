using ProductManagement.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Authors;

namespace ProductManagement.Web.Pages.Authors
{
    public class CreateModalModel : ProductManagementPageModel
    {
        [BindProperty]
        public AuthorCreateViewModel Author { get; set; }

        private readonly IAuthorsAppService _authorsAppService;

        public CreateModalModel(IAuthorsAppService authorsAppService)
        {
            _authorsAppService = authorsAppService;

            Author = new();
        }

        public async Task OnGetAsync()
        {
            Author = new AuthorCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _authorsAppService.CreateAsync(ObjectMapper.Map<AuthorCreateViewModel, AuthorCreateDto>(Author));
            return NoContent();
        }
    }

    public class AuthorCreateViewModel : AuthorCreateDto
    {
    }
}