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
    }
}



