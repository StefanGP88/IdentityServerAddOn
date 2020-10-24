using Ids.SimpleAdmin.Backend.Dtos;
using Microsoft.AspNetCore.Identity;
using System;

namespace Ids.SimpleAdmin.Backend.Mappers
{
    public static class UserMappers
    {
        public static IdentityUser MapToModel(this CreateUserRequestDto dto, UserManager<IdentityUser> userManager)
        {
            if (dto == null) return null;
            return new IdentityUser
            {
                AccessFailedCount = 0,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Email = dto.Email,
                EmailConfirmed = dto.ConfirmEmail,
                Id = Guid.NewGuid().ToString(),
                TwoFactorEnabled = dto.Use2Fa,
                PhoneNumber = dto.Phonenumber,
                PhoneNumberConfirmed = dto.ConfirmPhoneNumber,
                UserName = dto.Username,
                NormalizedEmail = userManager.NormalizeEmail(dto.Email),
                NormalizedUserName = userManager.NormalizeName(dto.Username),
                LockoutEnabled = dto.EnableLockout,
                LockoutEnd = null,
                SecurityStamp = Guid.NewGuid().ToString()
            };
        }
    }
}
