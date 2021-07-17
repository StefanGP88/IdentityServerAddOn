using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace Ids.SimpleAdmin.Backend
{
    public static class IdentityHelpers
    {
        public static AggregateException CheckResult(this IdentityResult userResult, string errorMessage)
        {
            if (userResult.Succeeded) return null;

            var errors = userResult.Errors
                .Select(x => new Exception(x.Description))
                .ToArray();
            return new AggregateException(errorMessage, errors);
        }
        public static void ThrowIfNull(this object _, params object[] objs)
        {
            if (objs is null) throw new ArgumentNullException(nameof(objs));
            foreach (var item in objs)
            {
                if (item is null) throw new ArgumentNullException(item.GetType().Name);
            }
        }
    }

}
