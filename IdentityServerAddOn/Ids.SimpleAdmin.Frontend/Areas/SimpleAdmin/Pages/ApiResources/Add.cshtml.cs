using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.ApiResources
{
    public class AddModel : BaseAddPage<ApiResourceContract>
    {
        public AddModel(IHandler<ApiResourceContract> handler) : base(handler) { }

        public PartialViewResult OnGetClaims(ApiResourceClaimsContract dto)
        {
            return Partial("_ClaimsTableRowPartial", dto);
        }
        public PartialViewResult OnGetProperties(ApiResourcePropertiesContract dto)
        {
            return Partial("_PropertiesTableRowPartial", dto);
        }
        public PartialViewResult OnGetScopes(ApiResourceScopesContract dto)
        {
            return Partial("_ScopesTableRowPartial", dto);
        }
        public PartialViewResult OnGetSecrets(ApiResourceSecretsContract dto)
        {
            return Partial("_SecretsTableRowPartial", dto);
        }
    }
}
