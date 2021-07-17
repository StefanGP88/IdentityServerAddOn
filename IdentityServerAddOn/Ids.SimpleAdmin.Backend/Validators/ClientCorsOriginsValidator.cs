using FluentValidation;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class ClientCorsOriginsValidator : EasyAdminValidatior<ClientCorsOriginsContract>
    {
        public ClientCorsOriginsValidator(ValidationCache cache) : base(cache)
        {
            RuleFor(x => x.Origin).MaximumLength(150).NotNull();
        }
    }
}
