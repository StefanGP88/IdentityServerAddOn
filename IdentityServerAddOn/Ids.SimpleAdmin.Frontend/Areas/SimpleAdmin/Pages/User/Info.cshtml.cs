using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.User
{
    public class InfoModel : BaseUserInfoPage
    {
        private readonly IHandler<RolesContract, string> _rolesHandler;
        public ListDto<RolesContract> Roles { get; set; }
        public InfoModel(IHandler<UserContract, string> handler, IHandler<RolesContract, string> rolesHandler) : base(handler)
        {
            _rolesHandler = rolesHandler;
        }


        public override async Task<IActionResult> OnGet(string id, CancellationToken cancel = default)
        {
            Roles = await _rolesHandler.GetAll(0, int.MaxValue, cancel).ConfigureAwait(false);
            return await base.OnGet(id, cancel).ConfigureAwait(false);
        }

        public override async Task<IActionResult> OnPost(UserContract dto, CancellationToken cancel = default)
        {
            Roles = await _rolesHandler.GetAll(0, int.MaxValue, cancel).ConfigureAwait(false);
            return await base.OnPost(dto, cancel).ConfigureAwait(false);
        }
    }

    public class BaseUserInfoPage : BaseInfoPage<UserContract, string>
    {
        public BaseUserInfoPage(IHandler<UserContract, string> handler) : base(handler) { }
    }
}
