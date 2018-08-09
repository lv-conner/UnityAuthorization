using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UnityAuthorization.Repository.Interface;
using Client = IdentityServer4.EntityFramework.Entities.Client;
using ModelClient = IdentityServer4.Models.Client;

namespace UnityAuthorization.Controllers
{
    public class ClientController : Controller
    {
        private readonly ConfigurationDbContext _configurationDbContext;
        private readonly IConfigurationDbContextRepository<Client> _configurationDbContextRepository;
        public ClientController(ConfigurationDbContext configurationDbContext, IConfigurationDbContextRepository<Client> configurationDbContextRepository)
        {
            _configurationDbContext = configurationDbContext;
            _configurationDbContextRepository = configurationDbContextRepository;
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
            return Json(clients);
        }

        public IActionResult Add(int Id,string clientName,string clientSecret)
        {
            var client = _configurationDbContext.Clients
                        .Include(p => p.AllowedCorsOrigins)
                        .Include(p => p.AllowedGrantTypes)
                        .Include(p => p.AllowedScopes)
                        .Include(p => p.Claims)
                        .Include(p => p.ClientSecrets)
                        .Include(p => p.IdentityProviderRestrictions)
                        .Include(p => p.PostLogoutRedirectUris)
                        .Include(p => p.Properties)
                        .Include(p => p.RedirectUris)
                        .First(p=>p.Id==Id).ToModel();
            client.ClientId = clientName;
            client.ClientName = clientName;
            client.ClientSecrets = new List<IdentityServer4.Models.Secret>();
            client.ClientSecrets.Add(new IdentityServer4.Models.Secret(clientSecret.Sha256()));
            
            var entity = client.ToEntity();
            _configurationDbContextRepository.Add(entity);
            _configurationDbContextRepository.SaveChange();
            return Json(entity.ToModel());
        }

        public ActionResult<Client> Detail(int id)
        {
            var client = _configurationDbContext.Clients.Find(id);
            _configurationDbContext.Entry(client)
                .Collection(p => p.AllowedCorsOrigins)
                .Load();
            _configurationDbContext.Entry(client)
                .Collection(p => p.AllowedScopes).Load();
            var col = _configurationDbContext.Entry(client).Collections;
            return client;
        }
    }
}