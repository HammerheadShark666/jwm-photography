namespace PhotographySite.Helpers;

public class FileHelper
{
    public static async Task<List<string>> SaveFilesToDirectoryAsync(List<IFormFile> files, string directoryPath)
    {           
        List<string> fileNames = new List<string>();

        foreach (var formFile in files)
        {
            if (formFile.Length > 0)
            { 
                fileNames.Add(formFile.FileName);                    
                using (var stream = new FileStream(directoryPath + formFile.FileName, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }
        }

        return fileNames;
    }
}