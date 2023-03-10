namespace PhotographySite.Helpers;

public class EnvironmentVariablesHelper
{
	public static string AzureStoragePhotosContainerUrl()
	{
		return Environment.GetEnvironmentVariable(Constants.AzureStorageContainerUrl) + "/photos/"; 
	}

    public static string AzureStorageConnectionString()
    {
        return Environment.GetEnvironmentVariable(Constants.AzureStorageConnectionString);
    }

    public static string DatabaseConnectionString()
    {
        return Environment.GetEnvironmentVariable(Constants.DatabaseConnectionString);
    }
}
