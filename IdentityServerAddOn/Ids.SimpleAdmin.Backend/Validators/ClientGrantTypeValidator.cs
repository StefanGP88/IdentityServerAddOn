using FluentValidation;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class ClientGrantTypeValidator : SimpleAdminValidatior<ClientGrantTypesContract>
    {
        public ClientGrantTypeValidator(ValidationCache cache) : base(cache)
        {
            RuleFor(x => x.GrantType).MaximumLength(250).MinimumLength(1).NotNull();
        }
    }
}
