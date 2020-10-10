using Microsoft.Extensions.DependencyInjection;
using NormalLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NormalLibrary
{
    public static class DenpendencyInjection
    {
        public static void AddIdentityServerAddOn(this IServiceCollection services)
        {
            services.AddScoped<IRoleHandler, RoleHandler>();
        }
    }
}
