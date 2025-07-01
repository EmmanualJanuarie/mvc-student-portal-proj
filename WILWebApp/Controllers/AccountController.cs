using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using WILWebApp.Entities;
using WILWebApp.Models;

namespace StudentPortal.Controllers
{
    public class AccountController : Controller
    {
        // Obtaining database context
        private readonly DB_Context _context;
        private readonly ILogger<AccountController> _logger;

        // UTILIZING CONSTRUCTOR TO SAVE DATA


        public AccountController(DB_Context dbContext, ILogger<AccountController> logger)
        {
            _context = dbContext;
            _logger = logger;
        }

        // Display Registration Page
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Handle Registration Post Request
        [HttpPost]
        public IActionResult Register(RegistrationViewModel model)
        {
            // VALIDATION CHECK - FOR USERS ACCOUNT
            if (ModelState.IsValid)
            {
                UserAccount account = new UserAccount();

                // ASSIGNING INCOMING - VIEW MODEL VALUES TO MODEL
                account.username = model.username;
                account.email = model.email;

                // Hash the password before saving
                account.pwd = BCrypt.Net.BCrypt.HashPassword(model.pwd);

                try
                {
                    _context.UserAccounts.Add(account);
                    _context.SaveChanges(); // SAVES DATABASE DATA

                    ModelState.Clear();
                    ViewBag.Message = $"{account.username} Registered Successfully. Please Login!";
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine(ex.InnerException?.Message); // Log the inner exception for more details
                    ModelState.AddModelError("", "Please enter a unique Email or Password");
                    return View(model);
                }
                return View();
            }

            return View(model);
        }

        // HANDLE Login Request
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // First, check if the user exists by username OR email
                var user = _context.UserAccounts.FirstOrDefault(x =>
                    x.username == model.username_or_email || x.email == model.username_or_email); // Make sure to use correct property names

                if (user != null && BCrypt.Net.BCrypt.Verify(model.pwd, user.pwd))
                {
                    // Clear any existing authentication
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    // Create claims
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Role, "User"),
                new Claim(ClaimTypes.Email, user.email),
                new Claim("UserId", user.Id.ToString()) // Add user ID if available
            };

                    // Create claims identity
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    // Configure authentication properties
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true, // This will create a persistent cookie
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24) // Cookie expires after 24 hours
                    };

                    // Sign in the user
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        claimsPrincipal,
                        authProperties);

                    // Log for debugging
                    _logger?.LogInformation($"User logged in: {user.username}, Email: {user.email}");

                    return RedirectToAction("Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username/email or password");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // LOGGING OUT
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // FOR REDIRECTION TO SECURE PAGE AFTER LOGIN
        [Authorize]
        public IActionResult Home()

        {
           
            return RedirectToAction("Index", "Home");
        }
    }
}

