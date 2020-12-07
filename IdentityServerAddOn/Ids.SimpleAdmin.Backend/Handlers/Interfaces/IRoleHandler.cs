using Ids.SimpleAdmin.Backend.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers.Interfaces
{
    public interface IRoleHandler
    {
        Task<RoleResponseDto> CreateRole(string roleName);

        Task<ListDto<RoleResponseDto>> ReadRoles(int page, int pageSize, CancellationToken cancel);
        Task<ListDto<RoleResponseDto>> ReadAllRoles(CancellationToken cancel);

        Task<RoleResponseDto> ReadRole(string id);

        Task<RoleResponseDto> UpdateRole(UpdateRoleRequestDto dto);

        Task DeleteRole(DeleteRoleRequestDto dto);
    }
}
