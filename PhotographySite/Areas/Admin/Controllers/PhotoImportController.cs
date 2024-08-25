using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Dto;
using PhotographySite.Dto.Response;
using PhotographySite.Helpers;

namespace PhotographySite.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
[Route("admin/photo")]
[AutoValidateAntiforgeryToken]
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
    public async Task<IActionResult> Import(List<IFormFile> files)
    {
        if (files == null || files.Count() == 0)
            return BadRequest(new BaseResponse()
            {
                IsValid = false,

                Messages = new List<Message>  {
                    new() { Severity = "error", Text = ConstantMessages.NoPhotosToImport}
                }
            });

        return PartialView("_ImportedPhotosGrid", await _photoImportService.ImportAsync(files));
    }
}