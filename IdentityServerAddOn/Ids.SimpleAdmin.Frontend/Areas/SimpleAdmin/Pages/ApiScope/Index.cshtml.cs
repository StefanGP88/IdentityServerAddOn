using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.ApiScope
{
    public class IndexModel : BaseIndexPage<ApiScopeContract, int?>
    {
        public IndexModel(IHandler<ApiScopeContract, int?> handler) : base(handler) { }
    }
}
