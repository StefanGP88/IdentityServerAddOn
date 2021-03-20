using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.ApiScope
{
    public class IndexModel : BaseIndexPage<ApiResourceContract, int?>
    {
        public IndexModel(IHandler<ApiResourceContract, int?> handler) : base(handler) { }
    }
}
