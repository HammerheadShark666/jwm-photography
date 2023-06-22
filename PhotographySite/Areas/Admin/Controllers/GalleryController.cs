using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Admin.Dtos;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
[Route("admin/gallery")]
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
        GalleriesDto galleriesDto = new()
        {			
			SelectedGallery = await _galleryService.GetGalleryAsync(id),
            SelectGalleryPhotos = await _galleryPhotoService.GetGalleryPhotosAsync(id),
            LookupsDto = await _photoCatalogService.GetLookupsAsync(),
            GalleryListDto = await _galleryService.GetGalleriesAsync(),
            PhotoListDto = await _photoCatalogService.GetPhotosPageAsync(new PhotoFilterDto()
            {
                PageIndex = 1,
                PageSize = 25,
            })
        };

        return View("Galleries", galleriesDto);
    }

    [HttpPost("search")]
    public async Task<JsonResult> SearchPhotosAsync([FromBody] SearchPhotosDto searchPhotosDto)
    {
        return new JsonResult(await _galleryService.SearchPhotosAsync(searchPhotosDto)); 
    }

    [HttpPost("save/name")]
    public async Task<JsonResult> SaveName([FromBody] GalleryNameDto galleryNameDto)
    {
        galleryNameDto = await _galleryService.SaveName(galleryNameDto);
        Response.StatusCode = galleryNameDto.IsValid ? 200 : 400;
        return new JsonResult(galleryNameDto);
    }

    [HttpPost("new/save")]
    public async Task<JsonResult> SaveNewGallery([FromBody] GalleryNameDto galleryNameDto)
    {
        galleryNameDto = await _galleryService.SaveNewGalleryAsync(galleryNameDto);

        Response.StatusCode = galleryNameDto.IsValid ? 200 : 400;
        return new JsonResult(galleryNameDto); 
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteGallery(long id)
    {
        await _galleryService.DeleteAsync(id);
        return Ok();
    }
}
