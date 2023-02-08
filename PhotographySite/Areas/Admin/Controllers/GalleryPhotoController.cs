using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Admin.Models;
using PhotographySite.Areas.Admin.Services.Interfaces;

namespace PhotographySite.Areas.Admin.Controllers;

[Area("Admin")]
[Route("admin/gallery-photos")]
public class GalleryPhotoController : Controller
{
    private IPhotoCatalogService _photoCatalogService; 
    private IGalleryPhotoService _galleryPhotoService;

    public GalleryPhotoController(IPhotoCatalogService photoCatalogService, IGalleryService galleryService, IGalleryPhotoService galleryPhotoService)
    {
        _photoCatalogService = photoCatalogService; 
        _galleryPhotoService = galleryPhotoService;
    } 

    [HttpPost("add")]
    public async Task<JsonResult> AddAsync([FromBody] GalleryPhotoDto galleryPhotoDto)
    {
        return new JsonResult(await _galleryPhotoService.AddPhotoToGalleryAsync(galleryPhotoDto));
    }

    [HttpPost("move")]
    public async Task<JsonResult> MoveAsync([FromBody] GalleryPhotoDto galleryPhotoDto)
    {
        return new JsonResult(await _galleryPhotoService.MovePhotoInGalleryAsync(galleryPhotoDto));
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemoveAsync([FromBody] GalleryPhotoDto galleryPhotoDto)
    {
        await _galleryPhotoService.RemovePhotoFromGalleryAsync(galleryPhotoDto);
        return Ok();
    }
}