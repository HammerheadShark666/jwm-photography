using PhotographySite.Helpers; 

namespace PhotographySite.Helper;

public class Base
{
    public Base(){}

    public string GetStorageConnection()
    {
        return EnvironmentVariablesHelper.AzureStorageConnectionString();
    }
}