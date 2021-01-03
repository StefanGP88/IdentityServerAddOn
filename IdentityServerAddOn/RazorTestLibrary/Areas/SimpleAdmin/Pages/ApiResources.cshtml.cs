using Ids.SimpleAdmin.Backend.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace RazorTestLibrary.Areas.SimpleAdmin.Pages
{
    public class ApiResourcesModel : BasePageModel<ApiResourceResponseDto>
    {
        public IActionResult OnGet(CancellationToken cancel = default)
        {
            List = new ListDto<ApiResourceResponseDto>
            {
                Page = PageNumber,
                PageSize = PageSize
            };
            return Page();
        }
        public IActionResult OnPostAdd([FromForm] CreateApiScopeRequestDto dto, CancellationToken cancel = default)
        {
            return Page();
        }

        public IActionResult OnPostEdit([FromForm] UpdateApiScopeRequestDto dto, CancellationToken cancel = default)
        {
            return Page();
        }

        public IActionResult OnPostDelete([FromForm] string id, CancellationToken cancel = default)
        {
            return Page();
        }
    }
}
