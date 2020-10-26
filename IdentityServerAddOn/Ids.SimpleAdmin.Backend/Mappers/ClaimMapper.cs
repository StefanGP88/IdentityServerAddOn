using Ids.SimpleAdmin.Backend.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Ids.SimpleAdmin.Backend.Mappers
{
    public static class ClaimMapper
    {
        public static Claim MapToModel(this CreateRoleClaimRequestDto dto)
        {
            if (dto == null) return null;
            return new Claim(dto.Type, dto.Value);
        }

        public static RoleClaimResponseDto MapToDto(this Claim claim, IdentityRole role)
        {
            if (claim == null) return null;
            return new RoleClaimResponseDto
            {
                Type = claim.Type,
                Value = claim.Value,
                Role = role.MapToClaimDto()
            };
        }

        public static RoleClaimRoleDto MapToClaimDto(this IdentityRole role)
        {
            if (role == null) return null;
            return new RoleClaimRoleDto
            {
                Id = role.Id,
                Name = role.Name,
                NormalizedName = role.NormalizedName
            };
        }
    }
}
