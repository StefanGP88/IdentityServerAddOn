using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.ApiResources
{
    public class InfoModel : BaseInfoPage<ApiResourceContract, int?>
    {
        public InfoModel(IHandler<ApiResourceContract, int?> handler) : base(handler) { }

        public PartialViewResult OnGetSecrets(ApiResourceSecretsContract model )
        {
            return OnGetPartial("Secrets", model);
        }
        public PartialViewResult OnGetScopes(ApiResourceScopesContract model )
        {
            return OnGetPartial("Scopes", model);
        }
        public PartialViewResult OnGetUserclaims(ApiResourceClaimsContract model )
        {
            return OnGetPartial("Userclaims", model);
        }
        public PartialViewResult OnGetProperties(ApiResourcePropertiesContract model)
        {
            return OnGetPartial("Properties", model);
        }
    }
}
