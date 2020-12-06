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
        public static UserResponseDto MapToDto(this IdentityUser user)
        {
            if (user == null) return null;
            return new UserResponseDto
            {
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd.GetValueOrDefault().UtcDateTime,
                Phonenumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                Use2Fa = user.TwoFactorEnabled,
                Userid = user.Id,
                Username = user.UserName,
                ConcurrencyStamp = user.ConcurrencyStamp
            };
        }
        public static IdentityUser UpdateModel(this IdentityUser user, UserManager<IdentityUser> userManager, UpdateUserRequestDto dto)
        {
            if (user == null) return null;
            user.Email = dto.Email;
            user.NormalizedEmail = userManager.NormalizeEmail(dto.Email);
            user.UserName = dto.Username;
            user.UserName = userManager.NormalizeName(dto.Username);
            user.PhoneNumber = dto.Phonenumber;
            user.TwoFactorEnabled = dto.Use2Fa;
            user.EmailConfirmed = dto.ConfirmEmail;
            user.PhoneNumberConfirmed = dto.ConfirmPhoneNumber;
            user.LockoutEnabled = dto.EnableLockout;
            user.ConcurrencyStamp = dto.ConcurrencyStamp;
            if (dto.EndLockout && user.LockoutEnd > DateTime.UtcNow)
            {
                user.LockoutEnd = DateTime.UtcNow;
            }

            return user;
        }
    }
}
