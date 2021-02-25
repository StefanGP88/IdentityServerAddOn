using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.ApiResources
{
    public class AddModel : BaseAddPage<ApiResourceContract>
    {
        public AddModel(IHandler<ApiResourceContract> handler) : base(handler) { }

        public PartialViewResult OnPostClaims(ApiResourceClaimsContract dto)
        {
            return Partial("_ClaimsTableRowPartial", dto);
        }
    }
}
