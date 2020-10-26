using Ids.SimpleAdmin.Backend.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers.Interfaces
{
    public interface IUserClaimHandler
    {
        Task<UserClaimResponseDto> CreateRoleClaim(CreateUserClaimRequestDto dto);
        Task RemoveRoleClaim(RemoveUserClaimRequestDto dto, CancellationToken cancel);
        Task<ListDto<UserClaimResponseDto>> GetUserClaims(string userId, int page, int pageSize);
    }
}
