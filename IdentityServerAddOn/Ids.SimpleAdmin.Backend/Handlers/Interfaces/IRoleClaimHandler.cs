using Ids.SimpleAdmin.Backend.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers.Interfaces
{
    public interface IRoleClaimHandler
    {
        Task<RoleClaimResponseDto> CreateRoleClaim(CreateRoleClaimRequestDto dto);
        Task RemoveRoleClaim(RemoveRoleClaimRequestDto dto, CancellationToken cancel);
        Task<ListDto<RoleClaimResponseDto>> GetRoleClaims(string roleId, int page, int pageSize);
    }
}
