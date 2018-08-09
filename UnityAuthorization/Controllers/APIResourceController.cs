using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UnityAuthorization.Controllers
{
    public class APIResourceController : Controller
    {
        public APIResourceController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}