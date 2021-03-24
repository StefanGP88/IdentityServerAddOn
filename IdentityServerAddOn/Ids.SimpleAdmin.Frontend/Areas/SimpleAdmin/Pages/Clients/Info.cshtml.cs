using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Clients
{
    public class InfoModel : BaseInfoPage<ClientsContract, int?>
    {
        public InfoModel(IHandler<ClientsContract, int?> handler) : base(handler) { }
        public PartialViewResult OnGetScopes(ClientScopeContract model)
        {
            return OnGetPartial("Scopes", model);
        }
        public PartialViewResult OnGetSecrets(ClientSecretsContract model)
        {
            return OnGetPartial("Secrets", model);
        }
        public PartialViewResult OnGetRedirectUris(ClientRedirectUriContract model)
        {
            return OnGetPartial("RedirectUris", model);
        }
        public PartialViewResult OnGetProperties(ClientPropertiesContract model)
        {
            return OnGetPartial("Properties", model);
        }
        public PartialViewResult OnGetPostLogoutRedirectUris(ClientPostLogoutRedirectUrisContract model)
        {
            return OnGetPartial("PostLogoutRedirectUris", model);
        }
    }
}
