using FluentValidation;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class RolesValidator : AbstractValidator<RolesContract>
    {
        public RolesValidator()
        {
            RuleFor(x => x.Name).MaximumLength(256);
            RuleForEach(x => x.RoleClaims).SetValidator(new RoleClaimsValidator());
        }
    }
    public class RoleClaimsValidator : AbstractValidator<RoleClaimsContract>
    {
    }
}
