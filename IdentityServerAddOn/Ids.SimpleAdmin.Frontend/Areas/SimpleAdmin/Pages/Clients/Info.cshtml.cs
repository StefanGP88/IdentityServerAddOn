using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Clients
{
    public class InfoModel : BaseInfoPage<ClientsContract, int?>
    {
        public InfoModel(IHandler<ClientsContract, int?> handler) : base(handler) { }
  
    }
}
