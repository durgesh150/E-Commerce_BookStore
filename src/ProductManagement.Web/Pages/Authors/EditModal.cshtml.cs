using ProductManagement.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ProductManagement.Authors;

namespace ProductManagement.Web.Pages.Authors
{
    public class EditModalModel : ProductManagementPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public AuthorUpdateViewModel Author { get; set; }

        private readonly IAuthorsAppService _authorsAppService;

        public EditModalModel(IAuthorsAppService authorsAppService)
        {
            _authorsAppService = authorsAppService;

            Author = new();
        }

        public async Task OnGetAsync()
        {
            var author = await _authorsAppService.GetAsync(Id);
            Author = ObjectMapper.Map<AuthorDto, AuthorUpdateViewModel>(author);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _authorsAppService.UpdateAsync(Id, ObjectMapper.Map<AuthorUpdateViewModel, AuthorUpdateDto>(Author));
            return NoContent();
        }
    }

    public class AuthorUpdateViewModel : AuthorUpdateDto
    {
    }
}