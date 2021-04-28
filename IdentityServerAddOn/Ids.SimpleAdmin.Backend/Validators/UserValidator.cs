using FluentValidation;
using FluentValidation.Validators;
using Ids.SimpleAdmin.Contracts;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class UserValidator : AbstractValidator<UserContract>
    {
        private readonly IdentityErrorDescriber _errorDescriber;
        private readonly IdentityOptions _options;

        public UserValidator(IdentityErrorDescriber errorsDescriber,
            IOptions<IdentityOptions> options)
        {
            _errorDescriber = errorsDescriber;
            _options = options.Value;

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
            RuleFor(x => x.AccessFailedCount);
            RuleFor(x => x.UserRoles);
            RuleFor(x => x.SetPassword).Custom(CheckPassword);
            RuleForEach(x => x.UserClaims).SetValidator(new UserClaimsValidator());
        }

        private void CheckPassword(string password, CustomContext context)
        {
            if (string.IsNullOrWhiteSpace(password))
                return ;

            if (_options.Password.RequireDigit && !password.Any(char.IsDigit))
                context.AddFailure(_errorDescriber.PasswordRequiresDigit().Description);

            if (_options.Password.RequireLowercase && !password.Any(char.IsLower))
                context.AddFailure(_errorDescriber.PasswordRequiresLower().Description);

            if (_options.Password.RequireUppercase && !password.Any(char.IsUpper))
                context.AddFailure(_errorDescriber.PasswordRequiresUpper().Description);

            if (_options.Password.RequireNonAlphanumeric && password.All(char.IsLetterOrDigit))
                context.AddFailure(_errorDescriber.PasswordRequiresNonAlphanumeric().Description);

            if (password.Length < _options.Password.RequiredLength)
                context.AddFailure(_errorDescriber.PasswordTooShort(_options.Password.RequiredLength).Description);

            if (password.Distinct().Count() < _options.Password.RequiredUniqueChars)
                context.AddFailure(_errorDescriber.PasswordRequiresUniqueChars(_options.Password.RequiredUniqueChars)
                    .Description);
        }

        private void AddError(CustomContext context, IEnumerable<string> errors)
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
            RuleFor(x => x.UserId).MaximumLength(450);
            RuleFor(x => x.ClaimType);
            RuleFor(x => x.ClaimValue);
        }
    }
}