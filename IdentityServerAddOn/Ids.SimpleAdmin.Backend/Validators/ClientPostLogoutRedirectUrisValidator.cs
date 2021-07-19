using FluentValidation;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class ClientPostLogoutRedirectUrisValidator : SimpleAdminValidatior<ClientPostLogoutRedirectUrisContract>
    {
        public ClientPostLogoutRedirectUrisValidator(ValidationCache cache) : base(cache)
        {
            RuleFor(x => x.PostLogoutRedirectUri).MinimumLength(1).MaximumLength(2000).NotNull();
        }
    }
}
