using Ids.SimpleAdmin.Backend.Dtos;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace RazorTestLibrary.Areas.SimpleAdmin.Pages
{
    public class ApiResourcesModel : BasePageModel<ApiResourceResponseDto>
    {
        private readonly IApiResourceHandler _hander;
        public ApiResourcesModel(IApiResourceHandler apiResourceHandler)
        {
            _hander = apiResourceHandler;
        }
        public IActionResult OnGet(CancellationToken cancel = default)
        {
            List =  _hander.ReadAll(PageNumber, PageSize, cancel).GetAwaiter().GetResult();
            return Page();
        }
        public IActionResult OnPostAdd([FromForm] CreateApiResourceRequestDto dto, CancellationToken cancel = default)
        {
            _hander.CreateApiResource(dto, cancel).GetAwaiter().GetResult();
            List = _hander.ReadAll(PageNumber, PageSize, cancel).GetAwaiter().GetResult();
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
        public PartialViewResult OnGetPropertyTableRow(string property, string propertyKey)
        {
            return Partial("_PropertyTableRow", new PropertRowModel {Property = property, PropertyKey = propertyKey });
        }
    }
}