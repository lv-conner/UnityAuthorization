using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UnitiyAuthorization.Services.IdentityServices
{
    public class UnityUserProfileService : IProfileService
    {
        private readonly ILogger<UnityUserProfileService> _logger;
        public UnityUserProfileService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<UnityUserProfileService>();
        }
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            _logger.LogInformation("Call GetProfileDataAsync");
            context.AddRequestedClaims(context.Subject.Claims);
            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            _logger.LogInformation("Call IsActiveAsync");
            context.IsActive = true;
            return Task.CompletedTask;
        }
    }
}
