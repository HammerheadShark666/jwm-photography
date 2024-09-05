using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Dto.Response;
using PhotographySite.Helpers;

namespace PhotographySite.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
[Route("admin/photo")]
[AutoValidateAntiforgeryToken]
public class PhotoImportController(IPhotoImportService photoImportService) : Controller
{
    [HttpGet("import")]
    public IActionResult Import()
    {
        return View("Import");
    }

    [HttpPost("import")]
    public async Task<IActionResult> Import(List<IFormFile> files)
    {
        if (files == null || files.Count == 0)
            return BadRequest(new BaseResponse()
            {
                IsValid = false,

                Messages = [
                    new() { Severity = "error", Text = ConstantMessages.NoPhotosToImport}
                ]
            });

        return PartialView("_ImportedPhotosGrid", await photoImportService.ImportAsync(files));
    }
}