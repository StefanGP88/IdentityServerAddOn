using System;
using System.Collections.Generic;

namespace Ids.SimpleAdmin.Contracts
{
    public class ClientsContract : Identifiable<int?>
    {
        public bool Enabled { get; set; }
        public string ClientId { get; set; }
        public string ProtocolType { get; set; }
        public bool RequireClientSecret { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public string ClientUri { get; set; }
        public string LogoUri { get; set; }
        public bool RequireConsent { get; set; }
        public bool AllowRememberConsent { get; set; }
        public bool AlwaysIncludeUserClaimsInToken { get; set; }//TODO: rename to 'AlwaysIncludeUserClaimsInIdToken'
        public bool RequirePkce { get; set; }
        public bool AllowPlainTextPkce { get; set; }
        public bool RequireRequestObject { get; set; }
        public bool AllowAccessTokensViaBrowser { get; set; }
        public string FrontChannelLogoutUri { get; set; }
        public bool FrontChannelSessionRequired { get; set; }//TODO: rename to 'FrontChannelLogoutSessionRequired'
        public string BackChannelLogoutUri { get; set; }
        public bool BackChannelLogoutSessionRequired { get; set; }
        public bool AllowOfflineAccess { get; set; }
        public int IdentityTokenLifetime { get; set; }
        public string AllowedIdentityTokenSigningAlgorithms { get; set; }
        public int AccessTokenLifetime { get; set; }
        public int AuthorizationCodeLifeTime { get; set; }
        public int? ConsentLifetime { get; set; }//TODO: make sure nulalble is not causing trouble in validators
        public int AbsoluteRefreshTokenLifetime { get; set; }
        public int SlidingRefreshTokenLifetime { get; set; }
        public int RefreshTokenUsage { get; set; }
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public int AccessTokenType { get; set; }
        public bool EnabledLocalLogin { get; set; }//TODO: rename to 'EnableLocalLogin'
        public bool IncludeJwtId { get; set; }
        public bool AlwaysSendClientClaims { get; set; }
        public string ClientClaimsPrefix { get; set; }
        public string PairWiseSubjectSalt { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; } //TODO: make sure nulalble is not causing trouble in validators
        public DateTime? LastAccessed { get; set; } //TODO: make sure nulalble is not causing trouble in validators
        public int? UserSsoLifetime { get; set; }//TODO: make sure nulalble is not causing trouble in validators
        public string UserCodeType { get; set; }
        public int DeviceCodeLifetime { get; set; }
        public bool NonEditable { get; set; }
        public List<ClientScopeContract> Scopes { get; set; }//TODO: rename to 'AllowedScopes'
        public List<ClientSecretsContract> Secrets { get; set; } // TODO: rename to 'ClientSecrets'
        public List<ClientRedirectUriContract> RedirectUris { get; set; }
        public List<ClientPropertiesContract> Properties { get; set; }
        public List<ClientPostLogoutRedirectUrisContract> PostLogoutRedirectUris { get; set; }
        public List<ClientIdPRestrictionsContract> IdPRestrictions { get; set; } //TODO: rename to 'IdentityProviderRestrictions
        public List<ClientGrantTypesContract> GrantTypes { get; set; } //TODO: rename to 'AllowedGrantTypes'
        public List<ClientCorsOriginsContract> CorsOrigins { get; set; } //TODO: rename to  'AllowedCorsOrigins'
        public List<ClientClaimsContract> Claims { get; set; }
    }
    public class ClientScopeContract : Identifiable<int?>
    {
        public int? ClientId { get; set; }
        public string Scope { get; set; }
    }
    public class ClientSecretsContract : Identifiable<int?>
    {
        public int? ClientId { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }//TODO: make sure nulalble is not causing trouble in validators
        public SecretTypeEnum Type { get; set; }//TODO: make sure enum instead of int is not causing trouble in validators
        public DateTime Created { get; set; }
    }
    public class ClientRedirectUriContract : Identifiable<int?>
    {
        public int? ClientId { get; set; }
        public string RedirectUri { get; set; }
    }
    public class ClientPropertiesContract : Identifiable<int?>
    {
        public int? ClientId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class ClientPostLogoutRedirectUrisContract : Identifiable<int?>
    {
        public int? ClientId { get; set; }
        public string PostLogoutRedirectUri { get; set; }
    }
    public class ClientIdPRestrictionsContract : Identifiable<int?>
    {
        public int? ClientId { get; set; }
        public string Provider { get; set; }
    }
    public class ClientGrantTypesContract : Identifiable<int?>
    {
        public int? ClientId { get; set; }
        public string GrantType { get; set; }
    }
    public class ClientCorsOriginsContract : Identifiable<int?>
    {
        public int? ClientId { get; set; }
        public string Origin { get; set; }
    }
    public class ClientClaimsContract : Identifiable<int?>
    {
        public int? ClientId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }

    //TODO: Should properbly not be here
    public static class SecretConstants
    {
        public static readonly Dictionary<SecretTypeEnum, string> SecretTypeNames = new()
        {
            { SecretTypeEnum.SharedSecretSha256, "SharedSecret (Sha256)" },
            { SecretTypeEnum.SharedSecretSha512, "SharedSecret (Sha512)" },
            { SecretTypeEnum.X509Base64, "X509Certificate (Base64)" },
            { SecretTypeEnum.X509Name, "X509Certificate (Name)" },
            { SecretTypeEnum.X509Thumbprint, "X509Certificate (Thumbprint)" }
        };


    }
    public enum SecretTypeEnum
    {
        SharedSecretSha256,
        SharedSecretSha512,
        X509Thumbprint,
        X509Name,
        X509Base64
    }
}
