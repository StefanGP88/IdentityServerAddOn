using Ids.SimpleAdmin.Backend.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Interfaces
{
    public interface IUserRoleHandler
    {
        Task AddRoleToUser(CreateUserRoleRequestDto dto);
        Task RemoveRoleFromUser(RemoveUserRoleRequestDto dto);
        Task<ListDto<RoleResponseDto>> GetUserRoles(string userId, int page, int pageSize, CancellationToken cancel);
    }
}
