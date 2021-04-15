using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace Ids.SimpleAdmin.Backend
{
    public static class IdentityHelpers
    {
        public static void CheckResult(this IdentityResult userResult, string errorMessage)
        {
            if (!userResult.Succeeded)
            {
                var errors = userResult.Errors
                    .Select(x => new Exception(x.Description))
                    .ToArray();
                throw new AggregateException(errorMessage, errors);
            }
        }
    }
}
