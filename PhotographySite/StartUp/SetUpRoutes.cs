namespace PhotographySite.StartUp;

public class SetUpRoutes
{
    public static void Setup(WebApplication app)
    {
        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );
        app.MapAreaControllerRoute("Site", "site", "{controller}/{action}");
        app.MapAreaControllerRoute("Admin", "admin", "{controller}/{action}");
    }
}
