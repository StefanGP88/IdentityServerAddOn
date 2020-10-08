using Microsoft.AspNetCore.Identity;
using NormalLibrary.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NormalLibrary.Interfaces
{
    public interface IRoleHandler
    {
        Task CreateRole(string roleName);

        Task CreateRole(IdentityRole role);

        Task<ICollection<RoleResponseDto>> ReadAllRoles(CancellationToken cancel);

        Task<RoleResponseDto> ReadRole(string id);
    }
}
