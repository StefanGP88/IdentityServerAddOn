using FluentValidation;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Validators
{

    public class ValueClaimValidator : SimpleAdminValidatior<ValueClaimsContract>
    {

        public ValueClaimValidator(ValidationCache cache):base(cache)
        {
            RuleFor(x => x.Type).MaximumLength(250).When(x => x.GetType() == typeof(ClientClaimsContract)).NotNull().When(x => x.GetType() == typeof(ClientClaimsContract)); //TODO: check if this is the correct way of doing conditional validation with fluent validator
            RuleFor(x => x.Value).MaximumLength(250).When(x => x.GetType() == typeof(ClientClaimsContract)).NotNull().When(x => x.GetType() == typeof(ClientClaimsContract));
        }
    }
}
