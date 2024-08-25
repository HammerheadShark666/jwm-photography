namespace PhotographySite.Helpers;

public class EnvironmentVariablesHelper
{
    public static string AzureStoragePhotosContainerUrl = Environment.GetEnvironmentVariable(Constants.AzureStorageContainerUrl) + "/photos/";
    public static string AzureStorageConnectionString = Environment.GetEnvironmentVariable(Constants.AzureStorageConnectionString);
    public static string TempPhotoDirectoryPath = Environment.GetEnvironmentVariable(Constants.TempPhotoDirectoryPath);
    public static string ApplicationInsightsInstrumentationKey = Environment.GetEnvironmentVariable(Constants.ApplicationInsightsInstrumentationKey);
    public static string ApplicationInsightsConnectionString = Environment.GetEnvironmentVariable(Constants.ApplicationInsightsConnectionString);
}