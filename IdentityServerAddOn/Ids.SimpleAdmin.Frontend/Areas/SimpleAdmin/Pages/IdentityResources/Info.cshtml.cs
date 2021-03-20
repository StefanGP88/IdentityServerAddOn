using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.IdentityResources
{
    public class InfoModel : BaseInfoPage<IdentityResourceContract, int?>
    {
        public InfoModel(IHandler<IdentityResourceContract, int?> handler) : base(handler) { }

        public PartialViewResult OnGetUserclaims(IdentityResourceClaimsContract model)
        {
            return OnGetPartial("Userclaims", model);
        }
        public PartialViewResult OnGetProperties(IdentityResourcePropertiesContract model)
        {
            return OnGetPartial("Properties", model);
        }
    }
}