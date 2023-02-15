using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Contexts;
using PhotographySite.Helpers;
using System.Configuration;

namespace PhotographySite.SetUp;

public class SetUpDatabaseContext
{
    public static void Setup(WebApplicationBuilder builder)
    { 
		builder.Services.AddDbContext<PhotographySiteDbContext>(o => o.UseSqlServer(EnvironmentVariablesHelper.DatabaseConnectionString()));
	}
}
