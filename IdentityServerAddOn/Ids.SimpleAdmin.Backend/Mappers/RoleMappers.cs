using Microsoft.AspNetCore.Identity;
using NormalLibrary.Dtos;

namespace NormalLibrary.Mappers
{
    public static class RoleMappers
    {
        public static RoleResponseDto MapToDto(this IdentityRole role)
        {
            if (role == null) return null;

            return new RoleResponseDto
            {
                ConcurrencyStamp = role.ConcurrencyStamp,
                Id = role.Id,
                NormalizedName = role.NormalizedName,
                RoleName = role.Name
            };
        }
    }
}
