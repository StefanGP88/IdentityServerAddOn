using FluentValidation;
using FluentValidation.Validators;
using Ids.SimpleAdmin.Contracts;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class UserValidator : AbstractValidator<UserContract>
    {

        //private readonly IIdentityValidator<string> _identityValidator;
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
            //RuleFor(x => x.ReplacePassword).Custom((x, context) => PasswordValidatorAsync(x, context).Wait());
            RuleForEach(x => x.UserClaims).SetValidator(new UserClaimsValidator());
        }

        //private async Task<bool> PasswordValidatorAsync(string password, CustomContext context)
        //{
        //    if (string.IsNullOrWhiteSpace(password))
        //        return true;

        //    var result = await _identityValidator.ValidateAsync(password).ConfigureAwait(false);
        //    if (!result.Succeeded)
        //    {
        //        AddError(context, result.Errors);
        //    }
        //    return result.Succeeded;
        //}

        private static void AddError(CustomContext context, IEnumerable<string> errors)
        {
            foreach (var item in errors)
            {
                context.AddFailure(item);
            }
        }
    }
    public class UserClaimsValidator : AbstractValidator<UserClaimsContract>
    {
        public UserClaimsValidator()
        {
            RuleFor(x => x.UserId).MaximumLength(450).NotNull();
            RuleFor(x => x.Type);
            RuleFor(x => x.Value);
        }
    }
}
