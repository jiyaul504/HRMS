using Employee.Interfaces;
using Employee.Models;
using Employee.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Employee.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        public static UserProfileViewModel userProfile;

        public AccountController(IUserService userService)
        {
            this.userService = userService;

            if (userProfile == null)
            {
                userProfile = new UserProfileViewModel();
            }
            this.userService = userService;

        }


        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext == null || HttpContext.User == null || HttpContext.User.Identity == null)
            {
                throw new ArgumentNullException("HttpContext", "HttpContext is NULL");
            }

            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return View(new Login() { Email = "admin@company.com", Password = "User1234" });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login input)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await userService.GetUserByLoginCredentials(input);

            if (user == null)
            {
                ModelState.AddModelError("Email", "Account does not exists.");
                return View(new Login() { Email = "admin@company.com", Password = "User1234" });
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.EmailAddress),
                    new Claim(ClaimTypes.Name, user.FirstName),
                };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            { };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
