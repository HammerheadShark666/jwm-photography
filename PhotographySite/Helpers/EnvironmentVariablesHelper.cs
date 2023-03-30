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
}
