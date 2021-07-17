using FluentValidation;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class SecretValidator : EasyAdminValidatior<SecretsContract>
    {
        public SecretValidator(ValidationCache cache) : base(cache)
        {
            RuleFor(x => x.Description).MaximumLength(1000).When(x => x.GetType() == typeof(ApiResourceSecretsContract));
            RuleFor(x => x.Description).MaximumLength(2000).When(x => x.GetType() == typeof(ClientSecretsContract)).NotNull().When(x => x.GetType() == typeof(ApiResourceSecretsContract)); //todo double check
            RuleFor(x => x.Value).MaximumLength(4000).NotNull();
            RuleFor(x => x.Type).NotNull();
            RuleFor(x => x.Created).NotNull();
        }
    }
}
