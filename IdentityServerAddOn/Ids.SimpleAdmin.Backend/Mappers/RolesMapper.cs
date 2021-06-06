using Ids.SimpleAdmin.Contracts;
using Microsoft.AspNetCore.Identity;
using System;

namespace Ids.SimpleAdmin.Backend.Mappers
{
    public class RolesMapper : AbstractMapper<RolesContract, IdentityRole>
    {
        public override RolesContract ToContract(IdentityRole model)
        {
            this.ThrowIfNull(model);
            return new RolesContract
            {
                ConcurrencyStamp = model.ConcurrencyStamp,
                NormalizedName = model.NormalizedName,
                Id = model.Id,
                Name = model.Name
            };
        }

        public override IdentityRole ToModel(RolesContract contract)
        {
            return UpdateModel(new(), contract);
        }

        public override IdentityRole UpdateModel(IdentityRole model, RolesContract contract)
        {
            this.ThrowIfNull(model, contract);
            if (model.ConcurrencyStamp != contract.ConcurrencyStamp)
                throw new Exception("Concurrencystamp not matching");
            model.ConcurrencyStamp = Guid.NewGuid().ToString();
            model.Name = contract.Name;
            model.NormalizedName = contract.NormalizedName.ToUpperInvariant();
            return model;
        }
    }
    public class IdentityRoleClaimMapper : AbstractMapper<RoleClaimsContract, IdentityRoleClaim<string>>
    {
        public override RoleClaimsContract ToContract(IdentityRoleClaim<string> model)
        {
            this.ThrowIfNull(model);
            return new RoleClaimsContract
            {
                ClaimValue = model.ClaimValue,
                ClaimType = model.ClaimType
            };
        }

        public override IdentityRoleClaim<string> ToModel(RoleClaimsContract contract)
        {
            return UpdateModel(new(), contract);
        }

        public override IdentityRoleClaim<string> UpdateModel(IdentityRoleClaim<string> model, RoleClaimsContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.ClaimType = contract.ClaimType;
            model.ClaimValue = contract.ClaimValue;
            return model;
        }
    }
}
