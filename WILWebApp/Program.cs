using System.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Controllers;
using WILWebApp.Entities;
using WILWebApp.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Register the DbContext with SQL Server connection string
builder.Services.AddDbContext<DB_Context>(options =>
{
    var connString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connString);
});

// Register HttpClient for dependency injection
builder.Services.AddHttpClient(); // This line registers HttpClient

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the timeout duration
    options.Cookie.HttpOnly = true; // Make the cookie HTTP only
    options.Cookie.IsEssential = true; // Make the session cookie essential
});

// Code to allow third-party logins (Facebook and Google)
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = "XXXX-XXXX-XXXX-XXX-XXXX"; //removed ClientId for security Purposes
    options.ClientSecret = "XXXX-XXXX-XXXX-XXX-XXXX";//removed Client Secret for security Purposes
    options.Scope.Add("email");
    options.Scope.Add("profile");
    options.SaveTokens = true; // Save tokens for later use
    options.CallbackPath = "/Home/GoogleResponse/";
})
.AddFacebook(options =>
{
    options.AppId = "XXXX-XXXX-XXXX-XXX-XXXX"; //removed AppId for security Purposes
    options.AppSecret = "XXXX-XXXX-XXXX-XXX-XXXX"; //removed app secret for security purposes
    options.Scope.Add("email");
    options.Scope.Add("public_profile");
    options.SaveTokens = true; // Save tokens for later use
    options.CallbackPath = "/Signin_facebook"; // Ensure this matches your redirect URI
})
.AddGitHub(options =>
{
    options.ClientId = "XXXX-XXXX-XXXX-XXX-XXXX"; //removed clientId for security Purposes
    options.ClientSecret = "XXXX-XXXX-XXXX-XXX-XXXX"; //removed Client Secret for security Purposes
    options.Scope.Add("email");
    options.Scope.Add("public_profile");
    options.SaveTokens = true; // Save tokens for later use
    options.CallbackPath = "/Home/GitHubResponse/"; // Ensure this matches your redirect URI
});

// Add SignalR and Response Compression Middleware services
builder.Services.AddSignalR();


   


builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

var app = builder.Build();
app.UseResponseCompression();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.MapHub<ChatHub>("/chathub"); // Endpoint for SignalR for chatHub

// Map the ReportHub to the specified endpoint
//app.MapHub<ReportHub>("/reportHub"); // Endpoint for SignalR for reportHub  


app.Run();
