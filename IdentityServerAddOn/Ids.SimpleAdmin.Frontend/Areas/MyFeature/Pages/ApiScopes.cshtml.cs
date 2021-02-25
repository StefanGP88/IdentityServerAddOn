using System.Threading;
using Ids.SimpleAdmin.Backend.Dtos;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages
{
    public class ApiScopesModel : BasePageModel<ApiScopeResponseDto, replacethis>
    {
        private readonly IApiScopeHandler _handler;
        public ApiScopesModel(IApiScopeHandler handler)
        {
            _handler = handler;
        }

        public IActionResult OnGet(CancellationToken cancel = default)
        {
            List = _handler.ReadAllApiScopes(PageNumber, PageSize, cancel).GetAwaiter().GetResult();
            return Page();
        }
        public IActionResult OnPostAdd([FromForm] CreateApiScopeRequestDto dto, CancellationToken cancel = default)
        {
            _ = _handler.CreateApiScope(dto, cancel).GetAwaiter().GetResult();
            List = _handler.ReadAllApiScopes(PageNumber, PageSize, cancel).GetAwaiter().GetResult();
            return Page();
        }

        public IActionResult OnPostEdit([FromForm] UpdateApiScopeRequestDto dto, CancellationToken cancel = default)
        {
            _ = _handler.UpdateApiScope(dto, cancel).GetAwaiter().GetResult();
            List = _handler.ReadAllApiScopes(PageNumber, PageSize, cancel).GetAwaiter().GetResult();
            return Page();
        }

        public IActionResult OnPostDelete([FromForm] string id, CancellationToken cancel = default)
        {
            _handler.DeleteApiScope(id, cancel).GetAwaiter().GetResult();
            List = _handler.ReadAllApiScopes(PageNumber, PageSize, cancel).GetAwaiter().GetResult();
            return Page();
        }
    }
    public class replacethis
    {

    }
}
