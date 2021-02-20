using Ids.SimpleAdmin.Backend.Dtos;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.ApiResources
{
    public class IndexModel : BaseIndexPage<ApiResourceResponseDto>
    {
        private readonly IApiResourceHandler _hander;
        public IndexModel(IApiResourceHandler apiResourceHandler)
        {
            _hander = apiResourceHandler;
        }
        public void OnGet()
        {
            List = _hander.ReadAll(PageNumber, PageSize, default).GetAwaiter().GetResult();
        }
    }
}
