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
public class GalleryController(IPhotoCatalogService photoCatalogService, IGalleryService galleryService, IGalleryPhotoService galleryPhotoService) : Controller
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Gallery(long id)
    {
        GalleriesResponse galleriesResponse = new()
        {
            SelectedGallery = await galleryService.GetGalleryAsync(id),
            SelectGalleryPhotos = await galleryPhotoService.GetGalleryPhotosAsync(id),
            LookupsResponse = await photoCatalogService.GetLookupsAsync(),
            Galleries = await galleryService.GetGalleriesAsync(),
            Photos = await photoCatalogService.GetPhotosPageAsync(new PhotoFilterRequest()
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
        return Ok(await galleryService.SearchPhotosAsync(searchPhotosRequest));
    }

    [HttpPost("save/name")]
    public async Task<IActionResult> UpdateGallery([FromBody] GalleryUpdateRequest galleryUpdateRequest)
    {
        return Ok(await galleryService.UpdateAsync(galleryUpdateRequest));
    }

    [HttpPost("new/save")]
    public async Task<IActionResult> SaveNewGallery([FromBody] GalleryUpdateRequest galleryUpdateRequest)
    {
        return Ok(await galleryService.AddAsync(galleryUpdateRequest));
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteGallery(int id)
    {
        return Ok(await galleryService.DeleteAsync(id));
    }
}