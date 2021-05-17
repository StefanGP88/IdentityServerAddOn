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
        public bool AlwaysIncludeUserClaimsInToken { get; set; }
        public bool RequirePkce { get; set; }
        public bool AllowPlainTextPkce { get; set; }
        public bool RequireRequestObject { get; set; }
        public bool AllowAccessTokensViaBrowser { get; set; }
        public string FrontChannelLogoutUri { get; set; }
        public bool FrontChannelSessionRequired { get; set; }
        public string BackChannelLogoutUri { get; set; }
        public bool BackChannelLogoutSessionRequired { get; set; }
        public bool AllowOfflineAccess { get; set; }
        public int IdentityTokenLifetime { get; set; }
        public string AllowedIdentityTokenSigningAlgorithms { get; set; }
        public int AccessTokenLifetime { get; set; }
        public int AuthorizationCodeLifeTime { get; set; }
        public int ConsentLifetime { get; set; }
        public int AbsoluteRefreshTokenLifetime { get; set; }
        public int SlidingRefreshTokenLifetime { get; set; }
        public int RefreshTokenUsage { get; set; }
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public int AccessTokenType { get; set; }
        public bool EnabledLocalLogin { get; set; }
        public bool IncludeJwtId { get; set; }
        public bool AlwaysSendClientClaims { get; set; }
        public string ClientClaimsPrefix { get; set; }
        public string PairWiseSubjectSalt { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime LastAccessed { get; set; }
        public int UserSsoLifetime { get; set; }
        public string UserCodeType { get; set; }
        public int DeviceCodeLifetime { get; set; }
        public bool NonEditable { get; set; }
        public List<ClientScopeContract> Scopes { get; set; }
        public List<ClientSecretsContract> Secrets { get; set; }
        public List<ClientRedirectUriContract> RedirectUris { get; set; }
        public List<ClientPropertiesContract> Properties { get; set; }
        public List<ClientPostLogoutRedirectUrisContract> PostLogoutRedirectUris { get; set; }
        public List<ClientIdPRestrictionsContract> IdPRestrictions { get; set; }
        public List<ClientGrantTypesContract> GrantTypes { get; set; }
        public List<ClientCorsOriginsContract> CorsOrigins { get; set; }
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
        public DateTime Expiration { get; set; }
        public SecretTypes Type { get; set; }
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
        public static readonly Dictionary<SecretTypes, string> SecretTypes = new()
        {
            { Contracts.SecretTypes.SharedSecretSha256, "SharedSecret (Sha256)" },
            { Contracts.SecretTypes.SharedSecretSha512, "SharedSecret (Sha512)" },
            { Contracts.SecretTypes.X509CertificateBase64, "X509CertificateBase64" },
            { Contracts.SecretTypes.X509Name, "X509Name" },
            { Contracts.SecretTypes.X509Thumbprint, "X509Thumbprint" }
        };
    }
    public enum SecretTypes
    {
        SharedSecretSha256,
        SharedSecretSha512,
        X509Thumbprint,
        X509Name,
        X509CertificateBase64
    }
}
