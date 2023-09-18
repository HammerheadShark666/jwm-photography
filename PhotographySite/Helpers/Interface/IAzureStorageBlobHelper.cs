namespace PhotographySite.Helpers.Interface
{
    public interface IAzureStorageBlobHelper
    {
        Task SaveBlobToAzureStorageContainerAsync(IFormFile file, string containerName, string fileName);
        Task SaveBlobsToAzureStorageContainerAsync(List<IFormFile> files, string containerName);
        Task DeleteBlobInAzureStorageContainerAsync(string fileName, string containerName);
    }
}