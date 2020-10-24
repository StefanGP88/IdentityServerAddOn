﻿using Ids.SimpleAdmin.Backend.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Interfaces
{
    public interface IRoleHandler
    {
        Task<RoleResponseDto> CreateRole(string roleName);

        Task<ListDto<RoleResponseDto>> ReadAllRoles(int page, int pageSize, CancellationToken cancel);

        Task<RoleResponseDto> ReadRole(string id);

        Task<RoleResponseDto> UpdateRole(UpdateRoleRequestDto dto);

        Task DeleteRole(DeleteRoleRequestDto dto);
    }
}
