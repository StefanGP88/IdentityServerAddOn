using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.ApiResources
{
    public class EditModel : BaseEditPage<ApiResourceContract, int>
    {
        public EditModel(IHandler<ApiResourceContract, int> handler) : base(handler) { }

    }
}
