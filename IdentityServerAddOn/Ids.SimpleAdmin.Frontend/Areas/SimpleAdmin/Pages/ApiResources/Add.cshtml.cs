using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.ApiResources
{
    public class AddModel : BaseAddPage<ApiResourceContract>
    {
        public AddModel(IHandler<ApiResourceContract> handler) : base(handler) { }
    }
}
