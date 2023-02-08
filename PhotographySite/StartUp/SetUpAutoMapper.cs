using PhotographySite.Helpers;
using System.Reflection;

namespace PhotographySite.StartUp;

public class SetUpAutoMapper
{
    public static void Setup(WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperProfile)));
    }
}
