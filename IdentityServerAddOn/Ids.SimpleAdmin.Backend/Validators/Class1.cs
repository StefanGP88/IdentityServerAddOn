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
    public class ClaimValidator : EasyAdminValidatior<ClaimsContract>
    {
        public ClaimValidator(ValidationCache cache) : base(cache)
        {
            RuleFor(x => x.Type).MaximumLength(200).NotNull();
        }
    }
    public class ScopeValidator : EasyAdminValidatior<ScopeContract>
    {
        public ScopeValidator(ValidationCache cache) : base(cache)
        {
            RuleFor(x => x.Scope).MaximumLength(200).NotNull();
        }
    }
    public class PropertyValidator : EasyAdminValidatior<PropertyContract>
    {
        public PropertyValidator(ValidationCache cache) : base(cache)
        {
            RuleFor(x => x.Key).MaximumLength(250).NotNull();
            RuleFor(x => x.Value).MaximumLength(2000).NotNull();
        }
    }

    public class ValueClaimValidator : EasyAdminValidatior<ValueClaimsContract>
    {

        public ValueClaimValidator(ValidationCache cache):base(cache)
        {
            RuleFor(x => x.Type).MaximumLength(250).When(x => x.GetType() == typeof(ClientClaimsContract)).NotNull().When(x => x.GetType() == typeof(ClientClaimsContract)); //TODO: check if this is the correct way of doing conditional validation with fluent validator
            RuleFor(x => x.Value).MaximumLength(250).When(x => x.GetType() == typeof(ClientClaimsContract)).NotNull().When(x => x.GetType() == typeof(ClientClaimsContract));
        }
    }
}
