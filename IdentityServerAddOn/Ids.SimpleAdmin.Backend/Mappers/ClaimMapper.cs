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

        public static ClaimRoleResponseDto MapToClaimDto(this IdentityRole role)
        {
            if (role == null) return null;
            return new ClaimRoleResponseDto
            {
                Id = role.Id,
                Name = role.Name,
                NormalizedName = role.NormalizedName
            };
        }

        public static Claim MapToModel(this CreateUserClaimRequestDto dto)
        {
            if (dto == null) return null;
            return new Claim(dto.Type, dto.Value);
        }

        public static UserClaimResponseDto MapToDto(this Claim claim, IdentityUser user)
        {
            if (claim == null) return null;
            return new UserClaimResponseDto
            {
                Type = claim.Type,
                Value = claim.Value,
                User = user.MapUserClaimResponseDto()
            };
        }

        public static ClaimUserResponseDto MapUserClaimResponseDto(this IdentityUser user)
        {
            if (user == null) return null;
            return new ClaimUserResponseDto
            {
                Id = user.Id,
                Name = user.UserName,
                NormalizedName = user.NormalizedUserName
            };
        }
    }
}
