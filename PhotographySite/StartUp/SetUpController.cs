using Newtonsoft.Json.Serialization;

namespace PhotographySite.StartUp;

public static class SetUpController
{
    public static void Setup(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
    }
}
