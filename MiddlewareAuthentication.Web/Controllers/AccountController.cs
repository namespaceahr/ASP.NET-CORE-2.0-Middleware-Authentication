using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MiddlewareAuthentication.Data;

namespace MiddlewareAuthentication.Web.Controllers
{
    public class AccountController : Controller
    {
        public List<User> Users { get; set; } = new List<User>
        {
        new User{UserName = "Rifat", UserPassword = "123456" ,UserRole="Administrator" },
        new User{UserName = "Salman", UserPassword = "123456" ,UserRole="User" }
        };

        public IActionResult Login(string returnUrl = null)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user, string returnUrl = null)
        {
            const string badUserNameOrPasswordMessage = "Username or password is incorrect.";
            if (user == null)
            {
                return BadRequest(badUserNameOrPasswordMessage);
            }
            var lookupUser = Users.Where(u => u.UserName == user.UserName).FirstOrDefault();

            if (lookupUser?.UserPassword != user.UserPassword)
            {
                return BadRequest(badUserNameOrPasswordMessage);
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, lookupUser.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role,lookupUser.UserRole));

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            if (returnUrl == null)
            {
                returnUrl = TempData["returnUrl"]?.ToString();
            }
            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return BadRequest("Access Denied!");
        }
    }
}