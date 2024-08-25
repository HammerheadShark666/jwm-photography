namespace PhotographySite.Helpers;

public class Constants
{
    public const string AzureStorageContainerUrl = "AZURE_STORAGE_URL";
    public const string AzureStorageConnectionString = "JWM_PHOTOGRAPHY_AZURE_STORAGE_CONNECTION_STRING";
    public const string AzureStorageContainerName = "photos";
    public const string DatabaseConnectionString = "AZURE_SQL_CONNECTIONSTRING";
    public const string TempPhotoDirectoryPath = "JWM_PHOTOGRAPHY_TEMP_PHOTO_DIRECTORY_PATH";
    public const string ApplicationInsightsInstrumentationKey = "APPLICATIONINSIGHTS_INSTRUMENTATION_KEY";
    public const string ApplicationInsightsConnectionString = "APPLICATIONINSIGHTS_CONNECTION_STRING";

    public const string EnumMessageTypeNameInformation = "Information";
    public const string EnumMessageTypeNameWarning = "Warning";
    public const string EnumMessageTypeNameError = "Error";

    public const string TemplatePath = "/images/";
    public const string PortraitTemplate = "PortraitTemplate.jpg";
    public const string SquareTemplate = "SquareTemplate.jpg";
    public const string LandscapeTemplate = "LandscapeTemplate.jpg";

    public const string FileContentType = "image/jpeg";

    public const string ValidationEventBeforeSave = "BeforeSave";
    public const string ValidationEventAfterSave = "AfterSave";
    public const string ValidationEventBeforeDelete = "BeforeDelete";
    public const string ValidationEventAfterDelete = "AfterDelete";
    public const string ValidationEventRecordExists = "RecordExists";
}