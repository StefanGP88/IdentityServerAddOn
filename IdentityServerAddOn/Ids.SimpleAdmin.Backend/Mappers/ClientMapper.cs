﻿using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;
using Ids.SimpleAdmin.Backend.Mappers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using System;
using System.Linq;

namespace Ids.SimpleAdmin.Backend.Mappers
{
    public class ClientMapper : AbstractMapper<ClientsContract, IdentityServer4.EntityFramework.Entities.Client>
    {
        private readonly IMapper<ClientIdPRestrictionsContract, ClientIdPRestriction> _idPRestriction;
        private readonly IMapper<ClientClaimsContract, IdentityServer4.EntityFramework.Entities.ClientClaim> _claim;
        private readonly IMapper<ClientCorsOriginsContract, ClientCorsOrigin> _corsOrigin;
        private readonly IMapper<PropertyContract, ClientProperty> _property;
        private readonly IMapper<ScopeContract, ClientScope> _scope;
        private readonly IMapper<ClientSecretsContract, ClientSecret> _secret;
        private readonly IMapper<ClientGrantTypesContract, ClientGrantType> _grantType;
        private readonly IMapper<ClientRedirectUriContract, ClientRedirectUri> _redirectUri;
        private readonly IMapper<ClientPostLogoutRedirectUrisContract, ClientPostLogoutRedirectUri> _postLogoutUri;

        public ClientMapper(IMapper<ClientIdPRestrictionsContract, ClientIdPRestriction> idPRestriction,
          IMapper<ClientClaimsContract, IdentityServer4.EntityFramework.Entities.ClientClaim> claim,
          IMapper<ClientCorsOriginsContract, ClientCorsOrigin> corsOrigin,
          IMapper<PropertyContract, ClientProperty> property,
          IMapper<ScopeContract, ClientScope> scope,
          IMapper<ClientSecretsContract, ClientSecret> secret,
          IMapper<ClientGrantTypesContract, ClientGrantType> grantType,
          IMapper<ClientRedirectUriContract, ClientRedirectUri> redirectUri,
          IMapper<ClientPostLogoutRedirectUrisContract, ClientPostLogoutRedirectUri> postLogoutUri)
        {
            _idPRestriction = idPRestriction;
            _claim = claim;
            _corsOrigin = corsOrigin;
            _property = property;
            _scope = scope;
            _secret = secret;
            _grantType = grantType;
            _redirectUri = redirectUri;
            _postLogoutUri = postLogoutUri;
        }

        public override ClientsContract ToContract(IdentityServer4.EntityFramework.Entities.Client model)
        {
            this.ThrowIfNull(model);
            return new ClientsContract
            {
                AbsoluteRefreshTokenLifetime = model.AbsoluteRefreshTokenLifetime,
                AccessTokenLifetime = model.AccessTokenLifetime,
                AccessTokenType = model.AccessTokenType,
                AllowAccessTokensViaBrowser = model.AllowAccessTokensViaBrowser,
                AllowedIdentityTokenSigningAlgorithms = model.AllowedIdentityTokenSigningAlgorithms,
                AllowOfflineAccess = model.AllowOfflineAccess,
                AllowPlainTextPkce = model.AllowPlainTextPkce,
                AllowRememberConsent = model.AllowRememberConsent,
                AlwaysIncludeUserClaimsInToken = model.AlwaysIncludeUserClaimsInIdToken,
                AlwaysSendClientClaims = model.AlwaysSendClientClaims,
                AuthorizationCodeLifeTime = model.AuthorizationCodeLifetime,
                BackChannelLogoutSessionRequired = model.BackChannelLogoutSessionRequired,
                BackChannelLogoutUri = model.BackChannelLogoutUri,
                ClientClaimsPrefix = model.ClientClaimsPrefix,
                ClientId = model.ClientId,
                ClientName = model.ClientName,
                ClientUri = model.ClientUri,
                ConsentLifetime = model.ConsentLifetime,
                Created = model.Created,
                Description = model.Description,
                DeviceCodeLifetime = model.DeviceCodeLifetime,
                Enabled = model.Enabled,
                EnabledLocalLogin = model.EnableLocalLogin,
                FrontChannelLogoutUri = model.FrontChannelLogoutUri,
                FrontChannelSessionRequired = model.FrontChannelLogoutSessionRequired,
                Id = model.Id,
                IdentityTokenLifetime = model.IdentityTokenLifetime,
                IncludeJwtId = model.IncludeJwtId,
                LastAccessed = model.LastAccessed,
                LogoUri = model.LogoUri,
                NonEditable = model.NonEditable,
                PairWiseSubjectSalt = model.PairWiseSubjectSalt,
                ProtocolType = model.ProtocolType,
                RefreshTokenExpiration = model.RefreshTokenExpiration,
                RefreshTokenUsage = model.RefreshTokenUsage,
                RequireClientSecret = model.RequireClientSecret,
                RequireConsent = model.RequireConsent,
                RequirePkce = model.RequirePkce,
                RequireRequestObject = model.RequireRequestObject,
                SlidingRefreshTokenLifetime = model.SlidingRefreshTokenLifetime,
                UpdateAccessTokenClaimsOnRefresh = model.UpdateAccessTokenClaimsOnRefresh,
                Updated = model.Updated,
                UserCodeType = model.UserCodeType,
                UserSsoLifetime = model.UserSsoLifetime,
                Claims = model.Claims?.ConvertAll(x => _claim.ToContract(x)),
                CorsOrigins = model.AllowedCorsOrigins?.ConvertAll(x => _corsOrigin.ToContract(x)),
                GrantTypes = model.AllowedGrantTypes?.ConvertAll(x => _grantType.ToContract(x)),
                IdPRestrictions = model.IdentityProviderRestrictions?.ConvertAll(x => _idPRestriction.ToContract(x)),
                PostLogoutRedirectUris = model.PostLogoutRedirectUris?.ConvertAll(x => _postLogoutUri.ToContract(x)),
                Properties = model.Properties?.ConvertAll(x => _property.ToContract(x)),
                RedirectUris = model.RedirectUris?.ConvertAll(x => _redirectUri.ToContract(x)),
                Scopes = model.AllowedScopes?.ConvertAll(x => _scope.ToContract(x)),
                Secrets = model.ClientSecrets?.ConvertAll(x => _secret.ToContract(x)) //TODO: Consider making overload of ConvertAll that takes null list into account i.e. ConvertAllOrDefault or something
            };
        }

        public override IdentityServer4.EntityFramework.Entities.Client ToModel(ClientsContract contract)
        {
            return UpdateModel(new IdentityServer4.EntityFramework.Entities.Client(), contract); //TODO: check that create date is set on this and all the other to model methods in this file
        }

        public override IdentityServer4.EntityFramework.Entities.Client UpdateModel(IdentityServer4.EntityFramework.Entities.Client model, ClientsContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.AbsoluteRefreshTokenLifetime = contract.AbsoluteRefreshTokenLifetime;
            model.AccessTokenLifetime = contract.AccessTokenLifetime;
            model.AccessTokenType = contract.AccessTokenType;
            model.AllowAccessTokensViaBrowser = contract.AllowAccessTokensViaBrowser;
            model.AllowedIdentityTokenSigningAlgorithms = contract.AllowedIdentityTokenSigningAlgorithms;
            model.AllowOfflineAccess = contract.AllowOfflineAccess;
            model.AllowPlainTextPkce = contract.AllowPlainTextPkce;
            model.AllowRememberConsent = contract.AllowRememberConsent;
            model.AlwaysIncludeUserClaimsInIdToken = contract.AlwaysIncludeUserClaimsInToken;
            model.AlwaysSendClientClaims = contract.AlwaysSendClientClaims;
            model.AuthorizationCodeLifetime = contract.AuthorizationCodeLifeTime;
            model.BackChannelLogoutSessionRequired = contract.BackChannelLogoutSessionRequired;
            model.BackChannelLogoutUri = contract.BackChannelLogoutUri;
            model.ClientClaimsPrefix = contract.ClientClaimsPrefix;
            model.ClientId = contract.ClientId;
            model.ClientName = contract.ClientName;
            model.ClientUri = contract.ClientUri;
            model.ConsentLifetime = contract.ConsentLifetime;
            model.Created = contract.Created;
            model.Description = contract.Description;
            model.DeviceCodeLifetime = contract.DeviceCodeLifetime;
            model.Enabled = contract.Enabled;
            model.EnableLocalLogin = contract.EnabledLocalLogin;
            model.FrontChannelLogoutUri = contract.FrontChannelLogoutUri;
            model.FrontChannelLogoutSessionRequired = contract.FrontChannelSessionRequired;
            model.IdentityTokenLifetime = contract.IdentityTokenLifetime;
            model.IncludeJwtId = contract.IncludeJwtId;
            model.LastAccessed = contract.LastAccessed;
            model.LogoUri = contract.LogoUri;
            model.NonEditable = contract.NonEditable;
            model.PairWiseSubjectSalt = contract.PairWiseSubjectSalt;
            model.ProtocolType = contract.ProtocolType;
            model.RefreshTokenExpiration = contract.RefreshTokenExpiration;
            model.RefreshTokenUsage = contract.RefreshTokenUsage;
            model.RequireClientSecret = contract.RequireClientSecret;
            model.RequireConsent = contract.RequireConsent;
            model.RequirePkce = contract.RequirePkce;
            model.RequireRequestObject = contract.RequireRequestObject;
            model.SlidingRefreshTokenLifetime = contract.SlidingRefreshTokenLifetime;
            model.UpdateAccessTokenClaimsOnRefresh = contract.UpdateAccessTokenClaimsOnRefresh;
            model.Updated = contract.Updated;
            model.UserCodeType = contract.UserCodeType;
            model.UserSsoLifetime = contract.UserSsoLifetime;

            model.Claims = _claim.UpdateList(model.Claims, contract.Claims);
            model.AllowedCorsOrigins = _corsOrigin.UpdateList(model.AllowedCorsOrigins, contract.CorsOrigins);
            model.AllowedGrantTypes = _grantType.UpdateList(model.AllowedGrantTypes, contract.GrantTypes);
            model.IdentityProviderRestrictions = _idPRestriction.UpdateList(model.IdentityProviderRestrictions, contract.IdPRestrictions);
            model.PostLogoutRedirectUris = _postLogoutUri.UpdateList(model.PostLogoutRedirectUris, contract.PostLogoutRedirectUris);
            model.Properties = _property.UpdateList(model.Properties, contract.Properties);
            model.RedirectUris = _redirectUri.UpdateList(model.RedirectUris, contract.RedirectUris);
            model.AllowedScopes = _scope.UpdateList(model.AllowedScopes, contract.Scopes);
            model.ClientSecrets = _secret.UpdateList(model.ClientSecrets, contract.Secrets);
            return model;
        }
    }
    public class IdPRestrictionMapper : AbstractMapper<ClientIdPRestrictionsContract, ClientIdPRestriction>
    {
        public override ClientIdPRestrictionsContract ToContract(ClientIdPRestriction model)
        {
            this.ThrowIfNull(model);
            return new ClientIdPRestrictionsContract
            {
                ClientId = model.Id,
                Id = model.Id,
                Provider = model.Provider
            };
        }

        public override ClientIdPRestriction ToModel(ClientIdPRestrictionsContract contract)
        {
            return UpdateModel(new(), contract);
        }

        public override ClientIdPRestriction UpdateModel(ClientIdPRestriction model, ClientIdPRestrictionsContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.Provider = contract.Provider;
            return model;
        }
    }
    public class ClientClaimMapper : AbstractMapper<ClientClaimsContract, IdentityServer4.EntityFramework.Entities.ClientClaim>
    {
        public override ClientClaimsContract ToContract(IdentityServer4.EntityFramework.Entities.ClientClaim model)
        {
            this.ThrowIfNull(model);
            return new ClientClaimsContract
            {
                Type = model.Type,
                Id = model.Id,
                Value = model.Value
            };
        }

        public override IdentityServer4.EntityFramework.Entities.ClientClaim ToModel(ClientClaimsContract contract)
        {
            return UpdateModel(new(), contract);
        }

        public override IdentityServer4.EntityFramework.Entities.ClientClaim UpdateModel(IdentityServer4.EntityFramework.Entities.ClientClaim model, ClientClaimsContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.Type = contract.Type;
            model.Value = contract.Value;
            return model;
        }
    }
    public class CorsOriginMapper : AbstractMapper<ClientCorsOriginsContract, ClientCorsOrigin>
    {
        public override ClientCorsOriginsContract ToContract(ClientCorsOrigin model)
        {
            this.ThrowIfNull(model);
            return new ClientCorsOriginsContract
            {
                ClientId = model.ClientId,
                Id = model.Id,
                Origin = model.Origin
            };
        }

        public override ClientCorsOrigin ToModel(ClientCorsOriginsContract contract)
        {
            return UpdateModel(new(), contract);
        }

        public override ClientCorsOrigin UpdateModel(ClientCorsOrigin model, ClientCorsOriginsContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.Origin = contract.Origin;
            return model;
        }
    }
    public class ClientPropertyMapper : AbstractMapper<PropertyContract, ClientProperty>
    {
        public override PropertyContract ToContract(ClientProperty model)
        {
            this.ThrowIfNull(model);
            return new PropertyContract
            {
                Key = model.Key,
                Value = model.Value,
                Id = model.Id
            };
        }

        public override ClientProperty ToModel(PropertyContract contract)
        {
            return UpdateModel(new(), contract);
        }

        public override ClientProperty UpdateModel(ClientProperty model, PropertyContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.Key = contract.Key;
            model.Value = contract.Value;
            return model;
        }
    }
    public class ClientScopeMapper : AbstractMapper<ScopeContract, ClientScope>
    {
        public override ScopeContract ToContract(ClientScope model)
        {
            this.ThrowIfNull(model);
            return new ScopeContract
            {
                Scope = model.Scope,
                Id = model.Id
            };
        }

        public override ClientScope ToModel(ScopeContract contract)
        {
            return UpdateModel(new(), contract);
        }

        public override ClientScope UpdateModel(ClientScope model, ScopeContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.Scope = contract.Scope;
            return model;
        }
    }
    public class ClientSecretsMapper : AbstractMapper<ClientSecretsContract, ClientSecret>
    {
        private readonly ConfigurationDbContext _confContext;
        public ClientSecretsMapper(ConfigurationDbContext configurationDbContext)
        {
            _confContext = configurationDbContext;
        }
        public override ClientSecretsContract ToContract(ClientSecret model)
        {
            this.ThrowIfNull(model);
            return new ClientSecretsContract
            {
                Id = model.Id,
                ClientId = model.ClientId,
                Created = model.Created,
                Description = model.Description,
                Expiration = model.Expiration,
                Type = SecretHelpers.GetSecretTypeEnum(model.Type, model.Value),
                Value = model.Value
            };
        }

        public override ClientSecret ToModel(ClientSecretsContract contract)
        {
            var model = UpdateModel(new(), contract);
            model.Created = DateTime.UtcNow;
            return model;
        }

        public override ClientSecret UpdateModel(ClientSecret model, ClientSecretsContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.Description = contract.Description;
            model.Expiration = contract.Expiration;
            model.Type = SecretHelpers.ConvertSecretTypeToString(contract.Type);
            model = UpdateSecretValue(model, contract);
            return model;
        }

        private ClientSecret UpdateSecretValue(ClientSecret model, ClientSecretsContract contract)
        {
            if (string.IsNullOrWhiteSpace(contract.Value)) return model;
            if (contract.Id is not null)
            {
                var isSame = _confContext.Clients
                    .Where(x => x.ClientSecrets.Any(y => y.Id == contract.Id && y.Value == contract.Value && y.Type == model.Type))
                    .Any();
                if (isSame) return model;
            }

            model.Value = SecretHelpers.GetHashedSecret(contract.Type, contract.Value);

            return model;
        }

    }
    public class GrantTypeMapper : AbstractMapper<ClientGrantTypesContract, ClientGrantType>
    {
        public override ClientGrantTypesContract ToContract(ClientGrantType model)
        {
            this.ThrowIfNull(model);
            return new ClientGrantTypesContract
            {
                ClientId = model.ClientId,
                GrantType = model.GrantType,
                Id = model.Id
            };
        }

        public override ClientGrantType ToModel(ClientGrantTypesContract contract)
        {
            return UpdateModel(new(), contract);
        }

        public override ClientGrantType UpdateModel(ClientGrantType model, ClientGrantTypesContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.GrantType = contract.GrantType;
            return model;
        }
    }
    public class RedirectUriMapper : AbstractMapper<ClientRedirectUriContract, ClientRedirectUri>
    {
        public override ClientRedirectUriContract ToContract(ClientRedirectUri model)
        {
            this.ThrowIfNull(model);
            return new ClientRedirectUriContract
            {
                ClientId = model.ClientId,
                Id = model.Id,
                RedirectUri = model.RedirectUri
            };
        }

        public override ClientRedirectUri ToModel(ClientRedirectUriContract contract)
        {
            return UpdateModel(new(), contract);
        }

        public override ClientRedirectUri UpdateModel(ClientRedirectUri model, ClientRedirectUriContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.RedirectUri = contract.RedirectUri;
            return model;
        }
    }
    public class PostLogoutRedirectUri : AbstractMapper<ClientPostLogoutRedirectUrisContract, ClientPostLogoutRedirectUri>
    {
        public override ClientPostLogoutRedirectUrisContract ToContract(ClientPostLogoutRedirectUri model)
        {
            this.ThrowIfNull(model);
            return new ClientPostLogoutRedirectUrisContract
            {
                Id = model.Id,
                ClientId = model.ClientId,
                PostLogoutRedirectUri = model.PostLogoutRedirectUri
            };
        }

        public override ClientPostLogoutRedirectUri ToModel(ClientPostLogoutRedirectUrisContract contract)
        {
            return UpdateModel(new(), contract);
        }

        public override ClientPostLogoutRedirectUri UpdateModel(ClientPostLogoutRedirectUri model, ClientPostLogoutRedirectUrisContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.PostLogoutRedirectUri = contract.PostLogoutRedirectUri;
            return model;
        }

    }
}
