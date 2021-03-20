using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.IdentityResources
{
    public class IndexModel : BaseIndexPage<IdentityResourceContract, int?>
    {
        public IndexModel(IHandler<IdentityResourceContract, int?> handler) : base(handler) { }
    }
}