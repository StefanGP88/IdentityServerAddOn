using Ids.SimpleAdmin.Backend.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers.Interfaces
{
    public interface IApiScopeHandler
    {
        Task<ApiScopeResponseDto> CreateApiScope(CreateApiScopeRequestDto dto, CancellationToken cancel);
        Task<ListDto<ApiScopeResponseDto>> ReadAllApiScopes(int page, int pageSize, CancellationToken cancel);
        //TODO: clean up
        //Task<ListDto<ApiScopeResponseDto>> ReadAllApiScopes(CancellationToken cancel);

        //Task<ApiScopeResponseDto> ReadApiScope(string id);

        //Task<ApiScopeResponseDto> UpdateApiScope(UpdateApiScopeRequestDto dto);

        //Task DeleteApiScope(DeleteApiScopeRequestDto dto);
    }
}
