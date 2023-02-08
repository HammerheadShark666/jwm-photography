using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Contexts;

namespace PhotographySite.SetUp;

public class SetUpDatabaseContext
{
    public static void Setup(WebApplicationBuilder builder)
    {        
        var connectionString = builder.Configuration.GetConnectionString("PhotographySiteDbConnection");
        builder.Services.AddDbContext<PhotographySiteDbContext>(o => o.UseSqlServer(connectionString));
    }
}
