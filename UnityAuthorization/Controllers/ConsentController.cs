using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UnityAuthorization.Attributes;
using UnityAuthorization.Models;

namespace UnityAuthorization.Controllers
{
    [Authorize]
    [SecurityHeaders]
    public class ConsentController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IResourceStore _resourceStore;
        private readonly ILogger<ConsentController> _logger;

        public ConsentController(IIdentityServerInteractionService interactionService, IClientStore clientStore, IResourceStore resourceStore, ILogger<ConsentController> logger)
        {
            _clientStore = clientStore;
            _interaction = interactionService;
            _resourceStore = resourceStore;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl)
        {
            //var request = await _interaction.GetAuthorizationContextAsync(returnUrl);
            //if (request != null)
            //{
            //    var client = await _clientStore.FindEnabledClientByIdAsync(request.ClientId);
            //    if (client != null)
            //    {
            //        var resources = await _resourceStore.FindEnabledResourcesByScopeAsync(request.ScopesRequested);
            //        if (resources != null && (resources.IdentityResources.Any() || resources.ApiResources.Any()))
            //        {
            //            return CreateConsentViewModel(model, returnUrl, request, client, resources);
            //        }
            //        else
            //        {
            //            _logger.LogError("No scopes matching: {0}", request.ScopesRequested.Aggregate((x, y) => x + ", " + y));
            //        }
            //    }
            //    else
            //    {
            //        _logger.LogError("Invalid client id: {0}", request.ClientId);
            //    }
            //}
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ConsentViewModel model,string returnUrl)
        {
            var request = await _interaction.GetAuthorizationContextAsync(returnUrl);
            ConsentResponse response = new ConsentResponse()
            {
                RememberConsent = true,
                ScopesConsented = request.ScopesRequested
            };
            await _interaction.GrantConsentAsync(request, response);
            return Redirect(returnUrl);
        }


        private ConsentViewModel CreateConsentViewModel(ConsentInputModel model, string returnUrl, AuthorizationRequest request,Client client, Resources resources)
        {
            var vm = new ConsentViewModel();
            vm.RememberConsent = model?.RememberConsent ?? true;
            vm.ScopesConsented = model?.ScopesConsented ?? Enumerable.Empty<string>();

            vm.ReturnUrl = returnUrl;

            vm.ClientName = client.ClientName ?? client.ClientId;
            vm.ClientUrl = client.ClientUri;
            vm.ClientLogoUrl = client.LogoUri;
            vm.AllowRememberConsent = client.AllowRememberConsent;

            vm.IdentityScopes = resources.IdentityResources.Select(x => CreateScopeViewModel(x, vm.ScopesConsented.Contains(x.Name) || model == null)).ToArray();
            vm.ResourceScopes = resources.ApiResources.SelectMany(x => x.Scopes).Select(x => CreateScopeViewModel(x, vm.ScopesConsented.Contains(x.Name) || model == null)).ToArray();
            if (resources.OfflineAccess)
            {
                vm.ResourceScopes = vm.ResourceScopes.Union(new ScopeViewModel[] {
                    GetOfflineAccessScope(vm.ScopesConsented.Contains(IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess) || model == null)
                });
            }

            return vm;
        }

        private ScopeViewModel GetOfflineAccessScope(bool check)
        {
            return new ScopeViewModel
            {
                Name = IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess,
                DisplayName = ConsentOptions.OfflineAccessDisplayName,
                Description = ConsentOptions.OfflineAccessDescription,
                Emphasize = true,
                Checked = check
            };
        }

        private ScopeViewModel CreateScopeViewModel(IdentityResource identity, bool check)
        {
            return new ScopeViewModel
            {
                Name = identity.Name,
                DisplayName = identity.DisplayName,
                Description = identity.Description,
                Emphasize = identity.Emphasize,
                Required = identity.Required,
                Checked = check || identity.Required
            };
        }

        public ScopeViewModel CreateScopeViewModel(Scope scope, bool check)
        {
            return new ScopeViewModel
            {
                Name = scope.Name,
                DisplayName = scope.DisplayName,
                Description = scope.Description,
                Emphasize = scope.Emphasize,
                Required = scope.Required,
                Checked = check || scope.Required
            };
        }
    }
}