using Ids.SimpleAdmin.Backend.Dtos;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading;
using System.Threading.Tasks;

namespace RazorTestLibrary.Areas.SimpleAdmin.Pages.Roles
{
    public class AllModel : PageModel
    {
        private readonly IRoleHandler _handler;
        public AllModel(IRoleHandler handler)
        {
            _handler = handler;
        }

        public ListDto<RoleResponseDto> List { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 0, int pageSize = 20, CancellationToken cancel = default)
        {
            List = await _handler.ReadAllRoles(pageNumber, pageSize, cancel).ConfigureAwait(false);
            return Page();
        }
        public IActionResult OnPostAdd([FromForm] string roleName, int pageNumber = 0, int pageSize = 25, CancellationToken cancel = default)
        {
            _ = _handler.CreateRole(roleName).GetAwaiter().GetResult();
            List = _handler.ReadAllRoles(pageNumber, pageSize, cancel).GetAwaiter().GetResult();
            return Page();
        }

        public IActionResult OnPostEdit(UpdateRoleRequestDto dto, int pageNumber = 0, int pageSize = 25, CancellationToken cancel = default)
        {
            _ = _handler.UpdateRole(dto).GetAwaiter().GetResult();
            List = _handler.ReadAllRoles(pageNumber, pageSize, cancel).GetAwaiter().GetResult();
            return Page();
        }

        public IActionResult OnPostDelete(DeleteRoleRequestDto dto, int pageNumber = 0, int pageSize = 25, CancellationToken cancel = default)
        {
            _handler.DeleteRole(dto).GetAwaiter().GetResult();
            List = _handler.ReadAllRoles(pageNumber, pageSize, cancel).GetAwaiter().GetResult();
            return Page();
        }
    }
}