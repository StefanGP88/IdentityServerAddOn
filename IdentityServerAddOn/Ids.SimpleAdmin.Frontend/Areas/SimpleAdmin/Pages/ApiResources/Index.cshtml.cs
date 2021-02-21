using Ids.SimpleAdmin.Backend.Dtos;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.ApiResources
{
    public class IndexModel : BaseIndexPage<ApiResourceResponseDto>
    {
        public IndexModel(IHandler<ApiResourceResponseDto> handler) : base(handler) { }
    }
}
