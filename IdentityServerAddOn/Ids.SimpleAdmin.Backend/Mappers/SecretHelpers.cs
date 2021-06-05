using IdentityServer4;
using IdentityServer4.Models;
using Ids.SimpleAdmin.Contracts;
using System;

namespace Ids.SimpleAdmin.Backend.Mappers
{
    public static class SecretHelpers
    {
        public static SecretTypeEnum GetSecretTypeEnum(string secretType, string secretValue)
        {
            const int sha256Length = 44;
            const int sha512Length = 88;
            return secretType switch
            {
                IdentityServerConstants.SecretTypes.X509CertificateBase64 => SecretTypeEnum.X509Base64,
                IdentityServerConstants.SecretTypes.X509CertificateName => SecretTypeEnum.X509Name,
                IdentityServerConstants.SecretTypes.X509CertificateThumbprint => SecretTypeEnum.X509Thumbprint,
                IdentityServerConstants.SecretTypes.SharedSecret when secretValue.Length == sha256Length => SecretTypeEnum.SharedSecretSha256,
                IdentityServerConstants.SecretTypes.SharedSecret when secretValue.Length == sha512Length => SecretTypeEnum.SharedSecretSha512,
                _ => throw new Exception("SecretType not defined")
            };
        }

        public static string ConvertSecretTypeToString(SecretTypeEnum secretTypeEnum)
        {
            return secretTypeEnum switch
            {
                SecretTypeEnum.X509Base64 => IdentityServerConstants.SecretTypes.X509CertificateBase64,
                SecretTypeEnum.X509Name => IdentityServerConstants.SecretTypes.X509CertificateName,
                SecretTypeEnum.X509Thumbprint => IdentityServerConstants.SecretTypes.X509CertificateThumbprint,
                SecretTypeEnum.SharedSecretSha256 => IdentityServerConstants.SecretTypes.SharedSecret,
                SecretTypeEnum.SharedSecretSha512 => IdentityServerConstants.SecretTypes.SharedSecret,
                _ => throw new Exception("SecretType not defined")
            };
        }

        public static string GetHashedSecret(SecretTypeEnum secretTypeEnum, string secretValue)
        {
            return secretTypeEnum switch
            {
                SecretTypeEnum.SharedSecretSha256 => secretValue.Sha256(),
                SecretTypeEnum.SharedSecretSha512 => secretValue.Sha512(),
                _ => secretValue
            };
        }
    }
}
