using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using PhotographySite.Helpers.Interface;

namespace SwanSong.Helper;

public class AzureStorageBlobHelper : Base, IAzureStorageBlobHelper
{
    public AzureStorageBlobHelper() : base()
    { }

    public async Task SaveBlobToAzureStorageContainerAsync(IFormFile file, string containerName, string fileName)
    {
        Stream fileStream = new MemoryStream();
        fileStream = file.OpenReadStream();
        var blobClient = new BlobContainerClient(GetStorageConnection(), containerName);
        var blob = blobClient.GetBlobClient(fileName);            
		    await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = "image/jpeg" });
        return;
    }

    public async Task SaveBlobsToAzureStorageContainerAsync(List<IFormFile> files, string containerName)
    { 
        foreach (IFormFile file in files) {        
            Stream fileStream = new MemoryStream();
            fileStream = file.OpenReadStream();
            var blobClient = new BlobContainerClient(GetStorageConnection(), containerName);
            var blob = blobClient.GetBlobClient(file.FileName);
            await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = "image/jpeg" }); 
        }
        return;
    }

    public async Task DeleteBlobInAzureStorageContainerAsync(string fileName, string containerName)
    {
        if (fileName == null) return;

        BlobServiceClient blobServiceClient = new BlobServiceClient(GetStorageConnection()); 
        BlobContainerClient container = blobServiceClient.GetBlobContainerClient(containerName);
        await container.DeleteBlobIfExistsAsync(fileName);

        return;
    }
}