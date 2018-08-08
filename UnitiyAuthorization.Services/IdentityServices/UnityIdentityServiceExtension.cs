using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UnitiyAuthorization.Services.IdentityServices;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UnityIdentityServiceExtension
    {
        public static IServiceCollection AddUnityProfileService(this IServiceCollection services)
        {
            services.AddTransient<IProfileService, UnityUserProfileService>();
            return services;
        }
    }
}
