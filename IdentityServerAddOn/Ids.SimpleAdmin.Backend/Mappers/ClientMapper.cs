using IdentityServer4.EntityFramework.Entities;
using Ids.SimpleAdmin.Backend.Mappers.Interfaces;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Mappers
{
    public class ClientMapper : IMapper<ClientsContract, Client>
    {
        private readonly IMapper<ClientIdPRestrictionsContract, ClientIdPRestriction> _idPRestriction;
        private readonly IMapper<ClientClaimsContract, ClientClaim> _claim;
        private readonly IMapper<ClientCorsOriginsContract, ClientCorsOrigin> _corsOrigin;
        private readonly IMapper<ClientPropertiesContract, ClientProperty> _property;
        private readonly IMapper<ClientScopeContract, ClientScope> _scope;
        private readonly IMapper<ClientSecretsContract, ClientSecret> _secret;
        private readonly IMapper<ClientGrantTypesContract, ClientGrantType> _grantType;
        private readonly IMapper<ClientRedirectUriContract, ClientRedirectUri> _redirectUri;
        private readonly IMapper<ClientPostLogoutRedirectUrisContract, ClientPostLogoutRedirectUri> _postLogoutUri;


        public ClientMapper(IMapper<ClientIdPRestrictionsContract, ClientIdPRestriction> idPRestriction,
          IMapper<ClientClaimsContract, ClientClaim> claim,
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

        public ClientsContract ToContract(Client model)
        {
            throw new System.NotImplementedException();
        }

        public Client ToModel(ClientsContract dto)
        {
            throw new System.NotImplementedException();
        }

        public Client UpdateModel(Client model, ClientsContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class IdPRestrictionMapper : IMapper<ClientIdPRestrictionsContract, ClientIdPRestriction>
    {
        public ClientIdPRestrictionsContract ToContract(ClientIdPRestriction model)
        {
            throw new System.NotImplementedException();
        }

        public ClientIdPRestriction ToModel(ClientIdPRestrictionsContract dto)
        {
            throw new System.NotImplementedException();
        }

        public ClientIdPRestriction UpdateModel(ClientIdPRestriction model, ClientIdPRestrictionsContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class ClientClaimMapper : IMapper<ClientClaimsContract, ClientClaim>
    {
        public ClientClaimsContract ToContract(ClientClaim model)
        {
            throw new System.NotImplementedException();
        }

        public ClientClaim ToModel(ClientClaimsContract dto)
        {
            throw new System.NotImplementedException();
        }

        public ClientClaim UpdateModel(ClientClaim model, ClientClaimsContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class CorsOriginMapper : IMapper<ClientCorsOriginsContract, ClientCorsOrigin>
    {
        public ClientCorsOriginsContract ToContract(ClientCorsOrigin model)
        {
            throw new System.NotImplementedException();
        }

        public ClientCorsOrigin ToModel(ClientCorsOriginsContract dto)
        {
            throw new System.NotImplementedException();
        }

        public ClientCorsOrigin UpdateModel(ClientCorsOrigin model, ClientCorsOriginsContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class ClientPropertyMapper : IMapper<ClientPropertiesContract, ClientProperty>
    {
        public ClientPropertiesContract ToContract(ClientProperty model)
        {
            throw new System.NotImplementedException();
        }

        public ClientProperty ToModel(ClientPropertiesContract dto)
        {
            throw new System.NotImplementedException();
        }

        public ClientProperty UpdateModel(ClientProperty model, ClientPropertiesContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class ClientScopeMapper : IMapper<ClientScopeContract, ClientScope>
    {
        public ClientScopeContract ToContract(ClientScope model)
        {
            throw new System.NotImplementedException();
        }

        public ClientScope ToModel(ClientScopeContract dto)
        {
            throw new System.NotImplementedException();
        }

        public ClientScope UpdateModel(ClientScope model, ClientScopeContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class ClientSecretsMapper : IMapper<ClientSecretsContract, ClientSecret>
    {
        public ClientSecretsContract ToContract(ClientSecret model)
        {
            throw new System.NotImplementedException();
        }

        public ClientSecret ToModel(ClientSecretsContract dto)
        {
            throw new System.NotImplementedException();
        }

        public ClientSecret UpdateModel(ClientSecret model, ClientSecretsContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class GrantTypeMapper : IMapper<ClientGrantTypesContract, ClientGrantType>
    {
        public ClientGrantTypesContract ToContract(ClientGrantType model)
        {
            throw new System.NotImplementedException();
        }

        public ClientGrantType ToModel(ClientGrantTypesContract dto)
        {
            throw new System.NotImplementedException();
        }

        public ClientGrantType UpdateModel(ClientGrantType model, ClientGrantTypesContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class RedirectUriMapper : IMapper<ClientRedirectUriContract, ClientRedirectUri>
    {
        public ClientRedirectUriContract ToContract(ClientRedirectUri model)
        {
            throw new System.NotImplementedException();
        }

        public ClientRedirectUri ToModel(ClientRedirectUriContract dto)
        {
            throw new System.NotImplementedException();
        }

        public ClientRedirectUri UpdateModel(ClientRedirectUri model, ClientRedirectUriContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class PostLogoutRedirectUri : IMapper<ClientPostLogoutRedirectUrisContract, ClientPostLogoutRedirectUri>
    {
        public ClientPostLogoutRedirectUrisContract ToContract(ClientPostLogoutRedirectUri model)
        {
            throw new System.NotImplementedException();
        }

        public ClientPostLogoutRedirectUri ToModel(ClientPostLogoutRedirectUrisContract dto)
        {
            throw new System.NotImplementedException();
        }

        public ClientPostLogoutRedirectUri UpdateModel(ClientPostLogoutRedirectUri model, ClientPostLogoutRedirectUrisContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
}
