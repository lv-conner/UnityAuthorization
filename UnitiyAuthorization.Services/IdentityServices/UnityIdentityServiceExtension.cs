using IdentityServer4.Services;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using UnityAuthorization.Services.IdentityServices;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UnityIdentityServiceExtension
    {
        public static IServiceCollection AddUnityProfileService(this IServiceCollection services)
        {
            services.AddTransient<IProfileService, UnityUserProfileService>();
            return services;
        }

        public static IServiceCollection AddRedisPersistentGrant(this IServiceCollection services)
        {
            services.AddTransient<IPersistedGrantStore, RedisPersistentGrantStore>();
            return services;
        }
    }
}
