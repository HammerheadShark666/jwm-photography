using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Contexts;
using PhotographySite.Helpers;

namespace PhotographySite.SetUp;

public static class SetUpDatabaseContext
{
    public static void Setup(WebApplicationBuilder builder)
    {          
        builder.Services.AddDbContext<PhotographySiteDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString(Constants.DatabaseConnectionString),
            options => options.EnableRetryOnFailure()));
    }
}