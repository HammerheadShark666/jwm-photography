using PhotographySite.Helpers.Exceptions;

namespace PhotographySite.Helpers;

public class EnvironmentVariablesHelper
{
    public static string AzureStoragePhotosContainerUrl => GetEnvironmentVariable(Constants.AzureStorageContainerUrl) + "/photos/";
    public static string AzureStorageConnectionString => GetEnvironmentVariable(Constants.AzureStorageConnectionString);
    public static string TempPhotoDirectoryPath => GetEnvironmentVariable(Constants.TempPhotoDirectoryPath);
    public static string ApplicationInsightsInstrumentationKey => GetEnvironmentVariable(Constants.ApplicationInsightsInstrumentationKey);
    public static string ApplicationInsightsConnectionString => GetEnvironmentVariable(Constants.ApplicationInsightsConnectionString);

    public static string GetEnvironmentVariable(string name)
    {
        var variable = Environment.GetEnvironmentVariable(name);

        if (string.IsNullOrEmpty(variable))
            throw new EnvironmentVariableNotFoundException($"Environment Variable Not Found: {name}.");

        return variable;
    }

}