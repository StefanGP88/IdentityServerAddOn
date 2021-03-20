using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.ApiScope
{
    public class InfoModel : BaseInfoPage<ApiScopeContract, int?>
    {
        public InfoModel(IHandler<ApiScopeContract, int?> handler) : base(handler) { }

        public PartialViewResult OnGetUserclaims(ApiScopeClaimsContract model)
        {
            return OnGetPartial("Userclaims", model);
        }
        public PartialViewResult OnGetProperties(ApiScopePropertiesContract model)
        {
            return OnGetPartial("Properties", model);
        }
    }
}
