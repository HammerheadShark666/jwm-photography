namespace PhotographySite.StartUp;

public static class SetUpMvc
{
    public static void Setup(WebApplicationBuilder builder)
    {
        builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
    }
}