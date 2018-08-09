using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Mvc;
using UnityAuthorization.Repository.Interface;

namespace UnityAuthorization.Controllers
{
    public class IdentityResourceController : Controller
    {
        IConfigurationDbContextRepository<ApiResource> _apiRepository;
        public IdentityResourceController(IConfigurationDbContextRepository<ApiResource>  apiRepository)
        {
            _apiRepository = apiRepository;
        }
        public IActionResult Search()
        {
            return View();
        }
    }
}