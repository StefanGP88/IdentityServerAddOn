using System.Linq;
using System.Security.Cryptography;
using FluentValidation;
using FluentValidation.Validators;
using Ids.SimpleAdmin.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class RolesValidator : AbstractValidator<RolesContract>
    {
        
        private readonly IdentityErrorDescriber _errorDescriber;
        private readonly IdentityDbContext _identityDbContext;
        public RolesValidator(IdentityErrorDescriber errorsDescriber,
            IdentityDbContext identityDbContext)
        {
            _errorDescriber = errorsDescriber;
            _identityDbContext = identityDbContext;
            
            RuleFor(x => x.Name).MaximumLength(256);
            RuleFor(x=> x.ConcurrencyStamp).Custom(CheckConcurrencyStamp);
            RuleForEach(x => x.RoleClaims).SetValidator(new RoleClaimsValidator());
        }
        private void CheckConcurrencyStamp(string concurrencyStamp, CustomContext context)
        {
            var user = (UserContract)context.InstanceToValidate;
            var isStampTheSame =  _identityDbContext.Roles
                .Any(x => x.Id == user.Id && x.ConcurrencyStamp == user.ConcurrencyStamp);
            
            if(!isStampTheSame)
                context.AddFailure(_errorDescriber.ConcurrencyFailure().Description);
        }
    }
    public class RoleClaimsValidator : AbstractValidator<RoleClaimsContract>
    {
    }
}
