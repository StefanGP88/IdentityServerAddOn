using IdentityServer4;
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
        private readonly IMapper<ClientPropertiesContract, ClientProperty> _property;
        private readonly IMapper<ClientScopeContract, ClientScope> _scope;
        private readonly IMapper<ClientSecretsContract, ClientSecret> _secret;
        private readonly IMapper<ClientGrantTypesContract, ClientGrantType> _grantType;
        private readonly IMapper<ClientRedirectUriContract, ClientRedirectUri> _redirectUri;
        private readonly IMapper<ClientPostLogoutRedirectUrisContract, ClientPostLogoutRedirectUri> _postLogoutUri;

        public ClientMapper(IMapper<ClientIdPRestrictionsContract, ClientIdPRestriction> idPRestriction,
          IMapper<ClientClaimsContract, IdentityServer4.EntityFramework.Entities.ClientClaim> claim,
          IMapper<ClientCorsOriginsContract, ClientCorsOrigin> corsOrigin,
          IMapper<ClientPropertiesContract, ClientProperty> property,
          IMapper<ClientScopeContract, ClientScope> scope,
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
            return UpdateModel(new IdentityServer4.EntityFramework.Entities.Client(), contract);
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
            this.ThrowIfNull(contract);
            return new ClientIdPRestriction
            {
                Provider = contract.Provider
            };
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
                ClientId = model.ClientId,
                Id = model.Id,
                Value = model.Value
            };
        }

        public override IdentityServer4.EntityFramework.Entities.ClientClaim ToModel(ClientClaimsContract contract)
        {
            this.ThrowIfNull(contract);
            return new IdentityServer4.EntityFramework.Entities.ClientClaim
            {
                Value = contract.Value,
                Type = contract.Type
            };
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
            this.ThrowIfNull(contract);
            return new ClientCorsOrigin
            {
                Origin = contract.Origin
            };
        }

        public override ClientCorsOrigin UpdateModel(ClientCorsOrigin model, ClientCorsOriginsContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.Origin = contract.Origin;
            return model;
        }
    }
    public class ClientPropertyMapper : AbstractMapper<ClientPropertiesContract, ClientProperty>
    {
        public override ClientPropertiesContract ToContract(ClientProperty model)
        {
            this.ThrowIfNull(model);
            return new ClientPropertiesContract
            {
                Key = model.Key,
                Value = model.Value,
                ClientId = model.ClientId,
                Id = model.Id
            };
        }

        public override ClientProperty ToModel(ClientPropertiesContract contract)
        {
            this.ThrowIfNull(contract);
            return new ClientProperty
            {
                Key = contract.Key,
                Value = contract.Value
            };
        }

        public override ClientProperty UpdateModel(ClientProperty model, ClientPropertiesContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.Key = contract.Key;
            model.Value = contract.Value;
            return model;
        }
    }
    public class ClientScopeMapper : AbstractMapper<ClientScopeContract, ClientScope>
    {
        public override ClientScopeContract ToContract(ClientScope model)
        {
            this.ThrowIfNull(model);
            return new ClientScopeContract
            {
                Scope = model.Scope,
                ClientId = model.ClientId,
                Id = model.Id
            };
        }

        public override ClientScope ToModel(ClientScopeContract contract)
        {
            return UpdateModel(new(), contract);
        }

        public override ClientScope UpdateModel(ClientScope model, ClientScopeContract contract)
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
                Type = GetSecretTypeEnum(model),
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
            model = UpdateSecretType(model, contract);
            model = UpdateSecretValue(model, contract);
            return model;
        }

        private static SecretTypeEnum GetSecretTypeEnum(ClientSecret model)
        {
            const int sha256Length = 44;
            const int sha512Length = 88;
            return model.Type switch
            {
                IdentityServerConstants.SecretTypes.X509CertificateBase64 => SecretTypeEnum.X509Base64,
                IdentityServerConstants.SecretTypes.X509CertificateName => SecretTypeEnum.X509Name,
                IdentityServerConstants.SecretTypes.X509CertificateThumbprint => SecretTypeEnum.X509Thumbprint,
                IdentityServerConstants.SecretTypes.SharedSecret when model.Value.Length == sha256Length => SecretTypeEnum.SharedSecretSha256,
                IdentityServerConstants.SecretTypes.SharedSecret when model.Value.Length == sha512Length => SecretTypeEnum.SharedSecretSha512,
                _ => throw new Exception("SecretType not defined")
            };
        }

        private static ClientSecret UpdateSecretType(ClientSecret model, ClientSecretsContract contract)
        {
            model.Type = contract.Type switch
            {
                SecretTypeEnum.X509Base64 => IdentityServerConstants.SecretTypes.X509CertificateBase64,
                SecretTypeEnum.X509Name => IdentityServerConstants.SecretTypes.X509CertificateName,
                SecretTypeEnum.X509Thumbprint => IdentityServerConstants.SecretTypes.X509CertificateThumbprint,
                SecretTypeEnum.SharedSecretSha256 => IdentityServerConstants.SecretTypes.SharedSecret,
                SecretTypeEnum.SharedSecretSha512 => IdentityServerConstants.SecretTypes.SharedSecret,
                _ => throw new Exception("SecretType not defined")
            };
            return model;
        }

        private ClientSecret UpdateSecretValue(ClientSecret model, ClientSecretsContract contract)
        {
            if (!string.IsNullOrWhiteSpace(contract.Value)) return model;
            if (contract.Id is not null)
            {
                var isChanged = _confContext.Clients
                    .Where(x => x.ClientSecrets.Any(y => y.Id == contract.Id && y.Value == contract.Value))
                    .Any();
                if (!isChanged) return model;
            }

            model.Value = contract.Type switch
            {
                SecretTypeEnum.SharedSecretSha256 => contract.Value.Sha256(),
                SecretTypeEnum.SharedSecretSha512 => contract.Value.Sha512(),
                _ => contract.Value
            };

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
