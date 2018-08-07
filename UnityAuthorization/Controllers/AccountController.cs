using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using UnityAuthorization.Models;
using Microsoft.AspNetCore.Http;
using IdentityModel;

namespace UnityAuthorization.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model,string returnUrl)
        {
            var props = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(120))
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(IdentityServerConstants.DefaultCookieAuthenticationScheme);
            var claims = new List<Claim> { new Claim(JwtClaimTypes.Subject, "1") };
            claimsIdentity.AddClaim(new Claim(JwtClaimTypes.Name, "alice"));
            //claimsIdentity.AddClaims(claims);
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, "001"));
            claimsIdentity.AddClaim(new Claim("sub", "1"));
            ClaimsPrincipal user = new ClaimsPrincipal(claimsIdentity);
            //var u = new TestUser
            //{
            //    SubjectId = "1",
            //    Username = "alice",
            //    Password = "password",

            //    Claims = new List<Claim>
            //        {
            //            new Claim("name", "Alice"),
            //            new Claim("website", "https://alice.com")
            //        }
            //};
            //await HttpContext.SignInAsync(u.SubjectId, u.Username, props);
            await HttpContext.SignInAsync(IdentityServerConstants.DefaultCookieAuthenticationScheme, user);
            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Redirect(returnUrl);
            }
        }
    }
}