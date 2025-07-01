using System.Diagnostics;
using AspNet.Security.OAuth.GitHub;
using AspNet.Security.OAuth.LinkedIn;
using Microsoft.AspNetCore.Mvc;
using WILWebApp.Models;
using Facebook;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Twitter;


namespace StudentPortal.Controllers
{
    public class HomeController : Controller
    {
        // Display the Landing Page
        public ActionResult Index()
        {
            // Retrieve the username from the session - google
            var google_Name = HttpContext.Session.GetString("google_username");
            ViewBag.GoogleUser = google_Name;

            // Retrieve the username from the session - facebook
            var facebook_Name = HttpContext.Session.GetString("facebook_username");
            ViewBag.FacebookUser = facebook_Name;

            // Retrieve the username from the session - github
            var github_Name = HttpContext.Session.GetString("github_username");
            ViewBag.GitHubGuest = github_Name;

            return View();
        }

        // Facebook Sign-In
        public IActionResult Signin_facebook()
        {
            var redirectUrl = Url.Action("FacebookResponse", "Home");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, FacebookDefaults.AuthenticationScheme);
        }

        // Facebook Authentication Response
        public async Task<IActionResult> FacebookResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (result?.Principal != null)
            {
                var name = result.Principal.FindFirst("name")?.Value;
                var email = result.Principal.FindFirst("email")?.Value;

                // Check if name and email are not null before setting in session
                if (!string.IsNullOrEmpty(name))
                {
                    HttpContext.Session.SetString("facebook_username", name);
                }
               

                if (!string.IsNullOrEmpty(email))
                {
                    HttpContext.Session.SetString("facebook_email", email);
                }
                

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", new { error = "An error occurred during login." });
        }

        // Google Sign-In
        public IActionResult Signin_google()
        {
            var redirectUrl = Url.Action("GoogleResponse", "Home");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        // Google Authentication Response
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (result?.Principal != null)
            {
                var name = result.Principal.FindFirst("name")?.Value;
                var email = result.Principal.FindFirst("email")?.Value;

                // Check if name and email are not null before setting in session
                if (!string.IsNullOrEmpty(name))
                {
                    HttpContext.Session.SetString("google_username", name);
                }
                

                if (!string.IsNullOrEmpty(email))
                {
                    HttpContext.Session.SetString("google_email", email);
                }
               
            }

            return RedirectToAction("Index", new { error = "An error occurred during login." });
        }

        // GitHub Sign-In
        public IActionResult Signin_github()
        {
            var redirectUrl = Url.Action("GitHubResponse", "Home");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GitHubAuthenticationDefaults.AuthenticationScheme);
        }

        // LinkedIn Authentication Response
        public async Task<IActionResult> GitHubResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (result?.Principal != null)
            {
                var name = result.Principal.FindFirst("name")?.Value;
                var email = result.Principal.FindFirst("email")?.Value; // Email may not be available from github

                // Check if name and email are not null before setting in session
                if (!string.IsNullOrEmpty(name))
                {
                    HttpContext.Session.SetString("github_username", name);
                }
                

                if (!string.IsNullOrEmpty(email))
                {
                    HttpContext.Session.SetString("github_email", email);
                }
                

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", new { error = "An error occurred during login." });
        }

        // Admin Dashboard Redirect
        public IActionResult AdminDashboard()
        {
            return View("Index");
        }
    }
}