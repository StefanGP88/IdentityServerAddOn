using Ids.SimpleAdmin.Backend.Dtos;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace RazorTestLibrary.Areas.SimpleAdmin.Pages.Roles
{
    public class AllModel : BasePageModel<RoleResponseDto>
    {
        private readonly IRoleHandler _handler;
        public AllModel(IRoleHandler handler)
        {
            _handler = handler;
        }

        public IActionResult OnGet(CancellationToken cancel = default)
        {
            List = _handler.ReadAllRoles(PageNumber, PageSize, cancel).GetAwaiter().GetResult();
            return Page();
        }
        public IActionResult OnPostAdd([FromForm] string roleName, CancellationToken cancel = default)
        {
            _ = _handler.CreateRole(roleName).GetAwaiter().GetResult();
            List = _handler.ReadAllRoles(PageNumber, PageSize, cancel).GetAwaiter().GetResult();
            return Page();
        }

        public IActionResult OnPostEdit([FromForm] UpdateRoleRequestDto dto, CancellationToken cancel = default)
        {
            _ = _handler.UpdateRole(dto).GetAwaiter().GetResult();
            List = _handler.ReadAllRoles(PageNumber, PageSize, cancel).GetAwaiter().GetResult();
            return Page();
        }

        public IActionResult OnPostDelete([FromForm] DeleteRoleRequestDto dto, CancellationToken cancel = default)
        {
            _handler.DeleteRole(dto).GetAwaiter().GetResult();
            List = _handler.ReadAllRoles(PageNumber, PageSize, cancel).GetAwaiter().GetResult();
            return Page();
        }
    }
}