using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.ApiResources
{
    public class IndexModel : BaseIndexPage<ApiResourceContract, int>
    {
        public IndexModel(IHandler<ApiResourceContract, int> handler) : base(handler) { }
    }
}
