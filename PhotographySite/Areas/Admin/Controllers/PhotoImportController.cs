using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Admin.Services.Interfaces;

namespace PhotographySite.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
[Route("admin/photo")]
public class PhotoImportController : Controller
{   
    private IPhotoImportService _photoImportService;

    public PhotoImportController(IPhotoImportService photoImportService)
    {
        _photoImportService = photoImportService; 
    }

    [HttpGet("import")]
    public IActionResult Import()
    {
        return View("Import");
    }

    [HttpPost("import")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Import(List<IFormFile> files)
    {       
        if(files == null || files.Count() == 0)         
            return BadRequest("No photos have been selected to be imported.");  
         
        return PartialView("_ImportedPhotosGrid", await _photoImportService.ImportAsync(files)); 
    }     
}