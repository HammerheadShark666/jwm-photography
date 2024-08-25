using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Dto.Request;

namespace PhotographySite.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
[Route("admin/gallery-photos")]
[AutoValidateAntiforgeryToken]
public class GalleryPhotoController : Controller
{
    private IGalleryPhotoService _galleryPhotoService;

    public GalleryPhotoController(IGalleryPhotoService galleryPhotoService)
    {
        _galleryPhotoService = galleryPhotoService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddAsync([FromBody] GalleryPhotoAddRequest galleryPhotoAddRequest)
    {
        return Ok(await _galleryPhotoService.AddPhotoToGalleryAsync(galleryPhotoAddRequest));
    }

    [HttpPost("move")]
    public async Task<IActionResult> MoveAsync([FromBody] GalleryPhotoAddRequest galleryPhotoAddRequest)
    {
        return Ok(await _galleryPhotoService.MovePhotoInGalleryAsync(galleryPhotoAddRequest));
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemoveAsync([FromBody] GalleryPhotoAddRequest galleryPhotoAddRequest)
    {
        await _galleryPhotoService.RemovePhotoFromGalleryAsync(galleryPhotoAddRequest);
        return Ok();
    }
}