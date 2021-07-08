using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class ValidationCache
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private Dictionary<object, ValidationResult> _cache;
        public ValidationCache(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
            _cache = new Dictionary<object, ValidationResult>();
        }

        public void AddResult(object obj, ValidationResult result)
        {
            System.Console.WriteLine(obj.GetHashCode() + " added");
            _cache[obj] = result;
        }

        public bool HasErrors(object obj)
        {
            return !_cache[obj].IsValid;
        }

        public bool HasErrors(List<object> objs)
        {
            return objs.Any(x => !_cache[x].IsValid);
        }

        public int ErrorCount(object obj)
        {
            return _cache[obj].Errors.Count;
        }

        public int ErrorCount(List<object> objs)
        {
            return objs.Aggregate(0, (total, current) =>
            {
                total += _cache[current].Errors.Count;
                return total;
            });
        }

        public List<string> ErrorsMessages(object obj)
        {
            return _cache[obj].Errors.Select(x => x.ErrorMessage).ToList();
        }

        public string ErrorsMessage(object obj, string propertyName)
        {
            return _cache[obj].Errors
                .Where(x => x.PropertyName == propertyName)
                .Select(x => x.ErrorMessage)
                .First();
        }
    }
}
