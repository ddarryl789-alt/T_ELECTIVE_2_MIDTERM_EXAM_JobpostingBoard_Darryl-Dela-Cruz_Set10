using jobpostingboard_Dela_Cruz.Data;
using jobpostingboard_Dela_Cruz.DTOs;
using jobpostingboard_Dela_Cruz.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace jobpostingboard_Dela_Cruz.Controllers
{
    public class AccountController : Controller
    {
        // REGISTER
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (FakeDbContext.Users.Any(x =>
                x.Email.Equals(model.Email, StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError(
                    "Email",
                    "Email is already registered.");

                return View(model);
            }

            var user = new User
            {
                Id = FakeDbContext.Users.Count + 1,
                FullName = model.FullName,
                Email = model.Email,
                Password = model.Password
            };

            FakeDbContext.Users.Add(user);

            await SignInUser(user);

            return RedirectToAction("Index", "Jobs");
        }

        // LOGIN
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = FakeDbContext.Users.FirstOrDefault(x =>
                x.Email.Equals(
                    model.Email,
                    StringComparison.OrdinalIgnoreCase)
                && x.Password == model.Password);

            if (user == null)
            {
                ModelState.AddModelError(
                    "",
                    "Invalid email or password.");

                return View(model);
            }

            await SignInUser(user);

            return RedirectToAction("Index", "Jobs");
        }

        // CREATE LOGIN COOKIE
        private async Task SignInUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);
        }

        // LOGOUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}