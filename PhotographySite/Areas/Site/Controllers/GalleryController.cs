using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;

namespace PhotographySite.Areas.Site.Controllers;

[Area("site")]
[Route("gallery")]
public class GalleryController : Controller
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    private IGalleryService _galleryService;

    public GalleryController(IUnitOfWork unitOfWork, IMapper mapper, IGalleryService galleryService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _galleryService = galleryService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Index(int id)
    {
        return View("Gallery", await _galleryService.GetGalleryAsync(id));
    }
}
