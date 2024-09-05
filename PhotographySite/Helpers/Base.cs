namespace PhotographySite.Helpers;

public class Base
{
    public Base() { }

    public string GetStorageConnection()
    {
        return EnvironmentVariablesHelper.AzureStorageConnectionString;
    }
}