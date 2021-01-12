using Ids.SimpleAdmin.Backend.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers.Interfaces
{
    public interface IApiResourceHandler
    {
        Task CreateApiResource(CreateApiResourceRequestDto dto, CancellationToken cancellation);
        Task<ListDto<ApiResourceResponseDto>> ReadAll(int page, int pageSize, CancellationToken cancel);
    }
}
