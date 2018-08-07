using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using UnityAuthorization.Models;

namespace UnityAuthorization.Controllers
{
    public class ConsentController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IResourceStore _resourceStore;
        public ConsentController(IIdentityServerInteractionService interactionService, IClientStore clientStore, IResourceStore resourceStore)
        {
            _clientStore = clientStore;
            _interaction = interactionService;
            _resourceStore = resourceStore;
        }
        [HttpGet]
        public IActionResult Index(string returnUrl)
        {
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
    }
}