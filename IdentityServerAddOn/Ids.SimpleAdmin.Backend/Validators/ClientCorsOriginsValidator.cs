using FluentValidation;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class ClientCorsOriginsValidator : SimpleAdminValidatior<ClientCorsOriginsContract>
    {
        public ClientCorsOriginsValidator(ValidationCache cache) : base(cache)
        {
            RuleFor(x => x.Origin).MinimumLength(1).MaximumLength(150).NotNull();
        }
    }
}
