namespace PhotographySite.Extensions;

public static class AppExtensions
{
    public static void ConfigureRoutes(this WebApplication app)
    {
        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );
        app.MapAreaControllerRoute("Site", "site", "{controller}/{action}");
        app.MapAreaControllerRoute("Admin", "admin", "{controller}/{action}");
    }
}