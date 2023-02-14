using PhotographySite.Helpers; 

namespace SwanSong.Helper;

public class Base
{
    public Base(){
    }

    public string GetStorageConnection()
    {
        return EnvironmentVariablesHelper.AzureStorageConnectionString();
    }
}
