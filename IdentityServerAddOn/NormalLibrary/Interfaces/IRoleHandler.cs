﻿using NormalLibrary.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NormalLibrary.Interfaces
{
    public interface IRoleHandler
    {
        Task<RoleResponseDto> CreateRole(string roleName);

        Task<ICollection<RoleResponseDto>> ReadAllRoles(CancellationToken cancel);

        Task<RoleResponseDto> ReadRole(string id);

        Task<RoleResponseDto> UpdateRole(UpdateRoleRequestDto dto);

        Task DeleteRole(DeleteRoleRequestDto dto);
    }
}
