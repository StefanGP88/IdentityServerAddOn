using FluentValidation;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class UserValidator : AbstractValidator<UserContract>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName).MaximumLength(256);
            RuleFor(x => x.NormalizedUserName).MaximumLength(256);
            RuleFor(x => x.Email).MaximumLength(256);
            RuleFor(x => x.NormalizedEmail).MaximumLength(256);
            RuleFor(x => x.EmailConfirmed).NotNull();
            RuleFor(x => x.ConcurrencyStamp);
            RuleFor(x => x.PhoneNumber);
            RuleFor(x => x.PhoneNumberConfirmed).NotNull();
            RuleFor(x => x.TwoFactorEnabled).NotNull();
            RuleFor(x => x.LockoutEnd);
            RuleFor(x => x.LockoutEnabled).NotNull();
            RuleFor(x => x.ResetAccessFailedCount).NotNull();
            RuleFor(x => x.UserRoles);
            RuleForEach(x => x.UserClaims).SetValidator(new UserClaimsValidator());
        }
    }
    public class UserClaimsValidator : AbstractValidator<UserClaimsContract>
    {
        public UserClaimsValidator()
        {
            RuleFor(x => x.UserId).MaximumLength(450).NotNull();
            RuleFor(x => x.ClaimType);
            RuleFor(x => x.ClaimValue);
        }
    }
}
