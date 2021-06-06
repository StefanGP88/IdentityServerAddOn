using IdentityServer4.EntityFramework.Entities;
using Ids.SimpleAdmin.Backend.Mappers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Mappers
{
    public class IdentityResourceMapper : AbstractMapper<IdentityResourceContract, IdentityResource>
    {
        private readonly IMapper<IdentityResourceClaimsContract, IdentityResourceClaim> _claim;
        private readonly IMapper<IdentityResourcePropertiesContract, IdentityResourceProperty> _property;
        public IdentityResourceMapper(IMapper<IdentityResourcePropertiesContract, IdentityResourceProperty> propertyMapper,
            IMapper<IdentityResourceClaimsContract, IdentityResourceClaim> claimsMapper)
        {
            _property = propertyMapper;
            _claim = claimsMapper;
        }
        public override IdentityResourceContract ToContract(IdentityResource model)
        {
            this.ThrowIfNull(model);
            return new IdentityResourceContract
            {
                UserClaims = model.UserClaims?.ConvertAll(_claim.ToContract),
                Created = model.Created,
                Description = model.Description,
                DisplayName = model.DisplayName,
                Emphasize = model.Emphasize,
                Enabled = model.Enabled,
                Id = model.Id,
                Name = model.Name,
                NonEditable = model.NonEditable,
                Properties = model.Properties?.ConvertAll(_property.ToContract),
                Required = model.Required,
                ShowInDiscoveryDocument = model.ShowInDiscoveryDocument,
                Updated = model.Updated
            };
        }

        public override IdentityResource ToModel(IdentityResourceContract contract)
        {
            var model = UpdateModel(new(), contract);
            model.Created = DateTime.UtcNow;
            return model;
        }

        public override IdentityResource UpdateModel(IdentityResource model, IdentityResourceContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.Description = contract.Description;
            model.DisplayName = contract.DisplayName;
            model.Emphasize = contract.Emphasize;
            model.Enabled = contract.Enabled;
            model.Name = contract.Name;
            model.NonEditable = contract.NonEditable;
            model.Required = contract.Required;
            model.ShowInDiscoveryDocument = contract.ShowInDiscoveryDocument;
            model.Updated = DateTime.UtcNow;
            model.Properties = _property.UpdateList(model.Properties, contract.Properties);
            model.UserClaims = _claim.UpdateList(model.UserClaims, contract.UserClaims);
            return model;
        }
    }
    public class IdentityResourceClaimMapper : AbstractMapper<IdentityResourceClaimsContract, IdentityResourceClaim>
    {
        public override IdentityResourceClaimsContract ToContract(IdentityResourceClaim model)
        {
            this.ThrowIfNull(model);
            return new IdentityResourceClaimsContract
            {
                Type = model.Type,
                IdentityResourceId = model.IdentityResourceId,
                Id = model.Id
            };
        }
        public override IdentityResourceClaim ToModel(IdentityResourceClaimsContract contract)
        {
            return UpdateModel(new(), contract);
        }
        public override IdentityResourceClaim UpdateModel(IdentityResourceClaim model, IdentityResourceClaimsContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.Type = contract.Type;
            return model;
        }
    }
    public class IdentityResourcePropertyMapper : AbstractMapper<IdentityResourcePropertiesContract, IdentityResourceProperty>
    {
        public override IdentityResourcePropertiesContract ToContract(IdentityResourceProperty model)
        {
            this.ThrowIfNull(model);
            return new IdentityResourcePropertiesContract
            {
                Key = model.Key,
                Value = model.Value,
                Id = model.Id,
                IdentityResourceId = model.IdentityResourceId
            };
        }
        public override IdentityResourceProperty ToModel(IdentityResourcePropertiesContract contract)
        {
            return UpdateModel(new(), contract);
        }
        public override IdentityResourceProperty UpdateModel(IdentityResourceProperty model, IdentityResourcePropertiesContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.Value = contract.Value;
            model.Key = contract.Key;
            return model;
        }
    }
}
