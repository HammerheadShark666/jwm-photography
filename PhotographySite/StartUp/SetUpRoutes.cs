namespace PhotographySite.StartUp;

public class SetUpRoutes
{
    public static void Setup(WebApplication app)
    {
        app.MapAreaControllerRoute("Site", "site", "{controller}/{action}");
        app.MapAreaControllerRoute("Admin", "admin", "{controller}/{action}");
    }
}
