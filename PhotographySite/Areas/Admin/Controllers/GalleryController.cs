using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Admin.Dto.Request;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Dto.Request;
using PhotographySite.Dto.Response;

namespace PhotographySite.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
[Route("admin/gallery")]
[AutoValidateAntiforgeryToken]
public class GalleryController : Controller
{
    private IPhotoCatalogService _photoCatalogService;
    private IGalleryService _galleryService;
    private IGalleryPhotoService _galleryPhotoService;

    public GalleryController(IPhotoCatalogService photoCatalogService, IGalleryService galleryService, IGalleryPhotoService galleryPhotoService)
    {
        _photoCatalogService = photoCatalogService;
        _galleryService = galleryService;
        _galleryPhotoService = galleryPhotoService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Gallery(long id)
    {
        GalleriesResponse galleriesResponse = new()
        {
            SelectedGallery = await _galleryService.GetGalleryAsync(id),
            SelectGalleryPhotos = await _galleryPhotoService.GetGalleryPhotosAsync(id),
            LookupsResponse = await _photoCatalogService.GetLookupsAsync(),
            Galleries = await _galleryService.GetGalleriesAsync(),
            Photos = await _photoCatalogService.GetPhotosPageAsync(new PhotoFilterRequest()
            {
                PageIndex = 1,
                PageSize = 25,
            })
        };

        return View("Galleries", galleriesResponse);
    }

    [HttpPost("search")]
    public async Task<IActionResult> SearchPhotosAsync([FromBody] SearchPhotosRequest searchPhotosRequest)
    {
        return Ok(await _galleryService.SearchPhotosAsync(searchPhotosRequest));
    }

    [HttpPost("save/name")]
    public async Task<IActionResult> UpdateGallery([FromBody] GalleryUpdateRequest galleryUpdateRequest)
    {
        return Ok(await _galleryService.UpdateAsync(galleryUpdateRequest));
    }

    [HttpPost("new/save")]
    public async Task<IActionResult> SaveNewGallery([FromBody] GalleryUpdateRequest galleryUpdateRequest)
    {
        return Ok(await _galleryService.AddAsync(galleryUpdateRequest));
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteGallery(int id)
    {
        return Ok(await _galleryService.DeleteAsync(id));
    }
}