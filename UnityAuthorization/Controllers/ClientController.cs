using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace UnityAuthorization.Controllers
{
    public class ClientController : Controller
    {
        private readonly ConfigurationDbContext _configurationDbContext;
        public ClientController(ConfigurationDbContext configurationDbContext)
        {
            _configurationDbContext = configurationDbContext;
        }
        public ActionResult<string> Index()
        {
            var clients = _configurationDbContext.Clients
                .Include(p => p.AllowedCorsOrigins)
                .Include(p => p.AllowedGrantTypes)
                .Include(p => p.AllowedScopes)
                .Include(p => p.Claims)
                .Include(p => p.ClientSecrets)
                .Include(p => p.IdentityProviderRestrictions)
                .Include(p => p.PostLogoutRedirectUris)
                .Include(p => p.Properties)
                .Include(p => p.RedirectUris).ToList();
            var idClients = clients.Select(p => p.ToModel());
            var client = idClients.Select(p => p.ToEntity());
            return Json(idClients);
        }
    }
}