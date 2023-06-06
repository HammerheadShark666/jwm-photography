using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using PhotographySite.Data.Contexts;
using PhotographySite.Models;

namespace PhotographySite.StartUp;

public class SetUpIdentity
{
    public static void Setup(WebApplicationBuilder builder)
    {
        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = true; 
        }) 

        .AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<PhotographySiteDbContext>()
        .AddDefaultTokenProviders();
         
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.AccessDeniedPath = "/AccessDenied";
            options.Cookie.Name = "jwmPhotography";
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            options.LoginPath = "/login"; 
            options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            options.SlidingExpiration = true;
        }); 
    }
}



