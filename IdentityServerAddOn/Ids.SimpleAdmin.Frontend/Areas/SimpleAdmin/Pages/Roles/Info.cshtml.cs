using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Roles
{
    public class InfoModel : BaseInfoPage<RolesContract, string>
    {
        public InfoModel(IHandler<RolesContract, string> handler) : base(handler) { }
        public PartialViewResult OnGetRoleClaims(AspNetIdentityClaimsContract model)
        {
            return OnGetPartial("RoleClaims", model);
        }
    }
}
