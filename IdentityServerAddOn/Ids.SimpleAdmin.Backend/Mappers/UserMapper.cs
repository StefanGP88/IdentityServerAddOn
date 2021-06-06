using Ids.SimpleAdmin.Contracts;
using Microsoft.AspNetCore.Identity;
using System;

namespace Ids.SimpleAdmin.Backend.Mappers
{
    public class UserMapper : AbstractMapper<UserContract, IdentityUser>
    {
        private readonly IPasswordHasher<IdentityUser> _pwHasher;
        public UserMapper(IPasswordHasher<IdentityUser> pwHasher)
        {
            _pwHasher = pwHasher;
        }
        public override UserContract ToContract(IdentityUser model)
        {
            this.ThrowIfNull(model);
            return new UserContract
            {
                AccessFailedCount = model.AccessFailedCount,
                ConcurrencyStamp = model.ConcurrencyStamp,
                Email = model.Email,
                EmailConfirmed = model.EmailConfirmed,
                Id = model.Id,
                LockoutEnabled = model.LockoutEnabled,
                LockoutEnd = model.LockoutEnd,
                NormalizedEmail = model.NormalizedEmail,
                NormalizedUserName = model.NormalizedUserName,
                PhoneNumber = model.PhoneNumber,
                PhoneNumberConfirmed = model.PhoneNumberConfirmed,
                TwoFactorEnabled = model.TwoFactorEnabled,
                UserName = model.UserName
            };
        }

        public override IdentityUser ToModel(UserContract contract)
        {
            return UpdateModel(new(), contract);
        }

        public override IdentityUser UpdateModel(IdentityUser model, UserContract contract)
        {
            this.ThrowIfNull(model, contract);
            if (model.ConcurrencyStamp != contract.ConcurrencyStamp)
                throw new Exception("Concurrencystamp not matching");
            model.ConcurrencyStamp = Guid.NewGuid().ToString();
            model.AccessFailedCount = contract.AccessFailedCount;
            model.Email = contract.Email;
            model.EmailConfirmed = contract.EmailConfirmed;
            model.LockoutEnabled = contract.LockoutEnabled;
            model.LockoutEnd = contract.LockoutEnd;
            model.NormalizedEmail = contract.Email.ToUpperInvariant();
            model.NormalizedUserName = contract.UserName.ToUpperInvariant();
            model.SecurityStamp = Guid.NewGuid().ToString();
            model.PasswordHash = _pwHasher.HashPassword(model, contract.SetPassword);
            model.PhoneNumber = contract.PhoneNumber;
            model.PhoneNumberConfirmed = contract.PhoneNumberConfirmed;
            model.TwoFactorEnabled = contract.TwoFactorEnabled;
            model.UserName = contract.UserName;
            return model;
        }
    }
    public class UserClaimsmapper : AbstractMapper<UserClaimsContract, IdentityUserClaim<string>>
    {
        public override UserClaimsContract ToContract(IdentityUserClaim<string> model)
        {
            this.ThrowIfNull(model);
            return new UserClaimsContract
            {
                ClaimType = model.ClaimType,
                ClaimValue = model.ClaimValue,
                Id = model.Id,
                UserId = model.UserId
            };
        }

        public override IdentityUserClaim<string> ToModel(UserClaimsContract contract)
        {
            return UpdateModel(new(), contract);
        }

        public override IdentityUserClaim<string> UpdateModel(IdentityUserClaim<string> model, UserClaimsContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.ClaimType = contract.ClaimType;
            model.ClaimValue = contract.ClaimValue;
            return model;
        }
    }
}
