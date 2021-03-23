using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Clients
{
    public class IndexModel : BaseIndexPage<ClientsContract, int?>
    {
        public IndexModel(IHandler<ClientsContract, int?> handler) : base(handler) { }
    }
}
