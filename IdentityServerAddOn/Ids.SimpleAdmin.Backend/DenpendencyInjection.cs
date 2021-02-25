﻿using Microsoft.Extensions.DependencyInjection;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Backend.Handlers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend
{
    public static class DenpendencyInjection
    {
        public static void AddIdentityServerAddOn<TContext>(this IServiceCollection sc) where TContext : IdentityDbContext
        {
            //TODO: make scoped
            sc.AddSingleton<IHandler<ApiResourceContract>, ApiResourceHandler>();
        }
    }
}
