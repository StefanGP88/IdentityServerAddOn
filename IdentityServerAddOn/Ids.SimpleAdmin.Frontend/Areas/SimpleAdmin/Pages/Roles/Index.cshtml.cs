using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Roles
{
    public class IndexModel : BaseIndexPage<RolesContract, string>
    {
        public IndexModel(IHandler<RolesContract, string> handler) : base(handler) { }
    }
}
