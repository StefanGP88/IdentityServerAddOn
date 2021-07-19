using FluentValidation;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class ClaimValidator : SimpleAdminValidatior<ClaimsContract>
    {
        public ClaimValidator(ValidationCache cache) : base(cache)
        {
            RuleFor(x => x.Type).MaximumLength(200).NotNull();
        }
    }
}
