using FluentValidation;
using FluentValidation.Validators;
using Ids.SimpleAdmin.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class UserValidator : AbstractValidator<UserContract>
    {
        private readonly IdentityErrorDescriber _errorDescriber;
        private readonly IdentityOptions _options;
        private readonly IdentityDbContext _identityDbContext;

        public UserValidator(IdentityErrorDescriber errorsDescriber,
            IOptions<IdentityOptions> options,
            IdentityDbContext identityDbContext)
        {
            _errorDescriber = errorsDescriber;
            _options = options.Value;
            _identityDbContext = identityDbContext;

            RuleFor(x => x.UserName).NotNull().MaximumLength(256).Custom(CheckUser);
            RuleFor(x => x.NormalizedUserName).MaximumLength(256);
            RuleFor(x => x.Email).MaximumLength(256).Custom(CheckEmail);
            RuleFor(x => x.NormalizedEmail).MaximumLength(256);
            RuleFor(x => x.EmailConfirmed).NotNull();
            RuleFor(x => x.ConcurrencyStamp).Custom(CheckConcurrencyStamp);
            RuleFor(x => x.PhoneNumber);
            RuleFor(x => x.PhoneNumberConfirmed).NotNull();
            RuleFor(x => x.TwoFactorEnabled).NotNull();
            RuleFor(x => x.LockoutEnd);
            RuleFor(x => x.LockoutEnabled).NotNull();
            RuleFor(x => x.AccessFailedCount);
            RuleFor(x => x.UserRoles);
            RuleFor(x => x.SetPassword).Custom(CheckPassword);
            RuleForEach(x => x.UserClaims).SetValidator(new AspNetIdentityClaimValidator());
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
        private void CheckUser(string user, CustomContext context)
        {
            if (string.IsNullOrWhiteSpace(user)) return;
            
            var restrictedChars = user.Distinct().Aggregate(string.Empty, (characters, x) =>
            {
                if (!_options.User.AllowedUserNameCharacters.Contains(x))
                    characters += x;
                return characters;
            });

            if (restrictedChars.Any())
                context.AddFailure(_errorDescriber.InvalidUserName(user).Description);
        }
        private void CheckEmail(string email, CustomContext context )
        {
            if (!_options.User.RequireUniqueEmail) return;
            var user = (UserContract)context.InstanceToValidate;
            var isEmailTaken =  _identityDbContext.Users
                .Any(x => x.Id != user.Id && x.Email == user.Email);
            
            if(isEmailTaken)
                context.AddFailure(_errorDescriber.DuplicateEmail(email).Description);
        }
        private void CheckConcurrencyStamp(string concurrencyStamp, CustomContext context)
        {
            var user = (UserContract)context.InstanceToValidate;
            if (user.Id is null) return;
            var isStampTheSame =  _identityDbContext.Users
                .Any(x => x.Id == user.Id && x.ConcurrencyStamp == user.ConcurrencyStamp);
            
            if(!isStampTheSame && !string.IsNullOrWhiteSpace(user.ConcurrencyStamp))
                context.AddFailure(_errorDescriber.ConcurrencyFailure().Description);
        }
    }
}