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
            model.ConcurrencyStamp = Guid.NewGuid().ToString();
            model.Name = contract.Name;
            model.NormalizedName = contract.Name.ToUpperInvariant();
            return model;
        }
    }
    public class IdentityRoleClaimMapper : AbstractMapper<AspNetIdentityClaimsContract, IdentityRoleClaim<string>>
    {
        public override AspNetIdentityClaimsContract ToContract(IdentityRoleClaim<string> model)
        {
            this.ThrowIfNull(model);
            return new AspNetIdentityClaimsContract
            {
                Value = model.ClaimValue,
                Type = model.ClaimType
            };
        }

        public override IdentityRoleClaim<string> ToModel(AspNetIdentityClaimsContract contract)
        {
            return UpdateModel(new(), contract);
        }

        public override IdentityRoleClaim<string> UpdateModel(IdentityRoleClaim<string> model, AspNetIdentityClaimsContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.ClaimType = contract.Type;
            model.ClaimValue = contract.Value;
            return model;
        }
    }
}
