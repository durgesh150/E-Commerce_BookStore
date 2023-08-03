using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProductManagement.Web.Pages.Shared
{
    public class LookupModal : ProductManagementPageModel
    {
        public string CurrentId { get; set; }
        public string CurrentDisplayName { get; set; }

        public LookupModal()
        {
            CurrentId = string.Empty;
            CurrentDisplayName = string.Empty;
        }

        public Task OnGetAsync(string currentId, string currentDisplayName)
        {
            CurrentId = currentId;
            CurrentDisplayName = currentDisplayName;

            return Task.CompletedTask;
        }
    }
}