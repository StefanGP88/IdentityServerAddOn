using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.ApiResources
{
    public class InfoModel : BaseInfoPage<ApiResourceContract, int?>
    {
        public InfoModel(IHandler<ApiResourceContract, int?> handler) : base(handler) { }
    }
}
