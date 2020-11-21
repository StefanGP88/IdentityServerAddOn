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

        [BindProperty]
        public ListDto<RoleResponseDto> Roles { get; set; }

        public async Task<IActionResult> OnGet(int page = 0, int pageSize = 25, CancellationToken cancel = default)
        {
            Roles = await _handler.ReadAllRoles(page, pageSize, cancel).ConfigureAwait(false);
            return Page();
        }

        public async Task<IActionResult> OnPost(string roleName, int page = 0, int pageSize = 25, CancellationToken cancel = default)
        {
            var continueWithTask = await Task.Factory
                .StartNew(() => _handler.CreateRole(roleName), TaskCreationOptions.AttachedToParent)
                .ContinueWith(_ => _handler.ReadAllRoles(page, pageSize, cancel)).ConfigureAwait(false);

            Roles = await continueWithTask.ConfigureAwait(false);
            return Page();
        }

        public async Task<IActionResult> OnPut(UpdateRoleRequestDto dto, int page = 0, int pageSize = 25, CancellationToken cancel = default)
        {
            var continueWithTask = await Task.Factory
                .StartNew(() => _handler.UpdateRole(dto), TaskCreationOptions.AttachedToParent)
                .ContinueWith(_ => _handler.ReadAllRoles(page, pageSize, cancel)).ConfigureAwait(false);

            Roles = await continueWithTask.ConfigureAwait(false);
            return Page();
        }

        public async Task<IActionResult> OnDelete(DeleteRoleRequestDto dto, int page = 0, int pageSize = 25, CancellationToken cancel = default)
        {
            var continueWithTask = await Task.Factory
                .StartNew(() => _handler.DeleteRole(dto), TaskCreationOptions.AttachedToParent)
                .ContinueWith(_ => _handler.ReadAllRoles(page, pageSize, cancel)).ConfigureAwait(false);

            Roles = await continueWithTask.ConfigureAwait(false);
            return Page();
        }
    }
}
