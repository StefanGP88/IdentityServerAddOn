using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.User
{
    public class IndexModel : BaseIndexPage<UserContract, string>
    {
        public IndexModel(IHandler<UserContract, string> handler) : base(handler)
        {
        }
    }
}
