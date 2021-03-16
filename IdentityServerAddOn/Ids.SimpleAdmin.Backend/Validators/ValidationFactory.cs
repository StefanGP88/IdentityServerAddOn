using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class ValidationFactory
    {
        private readonly IHttpContextAccessor _contexAccessor;
        public ValidationFactory(IHttpContextAccessor httpContextAccessor)
        {
            _contexAccessor = httpContextAccessor;
        }


        public ValidationResult Validate<T>( T model)
        {
            if (model == null)
                return new ValidationResult();

            var validator = _contexAccessor.HttpContext.RequestServices.GetRequiredService<IValidator<T>>();
            var validationResult = validator.Validate(model);

            var dictionary = validationResult.Errors
                .Select(x => x.PropertyName)
                .ToDictionary(x => x, _ => new List<string>());

            for (int i = 0; i < validationResult.Errors.Count; i++)
            {
                var mesage = validationResult.Errors[i].ErrorMessage;
                dictionary[validationResult.Errors[i].PropertyName].Add(mesage);
            }

            return new ValidationResult(dictionary);
        }
    }
}
