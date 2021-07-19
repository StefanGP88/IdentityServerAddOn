using FluentValidation;
using FluentValidation.Validators;
using Ids.SimpleAdmin.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class RolesValidator : SimpleAdminValidatior<RolesContract>
    {
        
        private readonly IdentityErrorDescriber _errorDescriber;
        private readonly IdentityDbContext _identityDbContext;
        private readonly IValidator<ValueClaimsContract> _valueClaimsContract;
        public RolesValidator(IdentityErrorDescriber errorsDescriber,
            IdentityDbContext identityDbContext,
            ValidationCache cache,
            IValidator<ValueClaimsContract> valueClaimsContract) : base(cache)
        {
            _errorDescriber = errorsDescriber;
            _identityDbContext = identityDbContext;
            _valueClaimsContract = valueClaimsContract;

            RuleFor(x => x.Name).MaximumLength(256);
            RuleFor(x=> x.ConcurrencyStamp).Custom(CheckConcurrencyStamp);
            RuleForEach(x => x.Claims).SetValidator(_valueClaimsContract);
        }
        private void CheckConcurrencyStamp(string concurrencyStamp, CustomContext context)
        {
            var role = (RolesContract)context.InstanceToValidate;
            if (role.Id is null) return;
            var isStampTheSame =  _identityDbContext.Roles
                .Any(x => x.Id == role.Id && x.ConcurrencyStamp == role.ConcurrencyStamp);
            
            if(!isStampTheSame)
                context.AddFailure(_errorDescriber.ConcurrencyFailure().Description);
        }
    }
}
