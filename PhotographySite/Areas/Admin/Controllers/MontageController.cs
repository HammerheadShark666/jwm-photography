using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
[Route("admin/montage")]
public class MontageController : Controller
{   
    private IMontageService _montageService;

    public MontageController(IMontageService montageService)
    {      
        _montageService = montageService;
    }

    [HttpGet("")]
    public async Task<IActionResult> MontageManager()
    {         
        return View("Montage", await _montageService.GetMontageTemplatesAsync());
    }

    [HttpPost("add-image")]
    public async Task<JsonResult> AddMontageImageAsync([FromBody] MontageDto montageDto)
    {
        return new JsonResult(await _montageService.AddImageTemplateAsync(montageDto.Column, montageDto.Order, montageDto.Orientation));
    }

    [HttpPost("move-image")]
    public async Task MoveMontageImageAsync([FromBody] MontageDto montageDto)
    {
        await _montageService.MoveImageTemplateAsync(montageDto.Id, montageDto.Column, montageDto.Order); 
    }

    [HttpDelete("delete-image/{id:int}")]
    public async Task DeleteMontageImageAsync(int id)
    {
        await _montageService.DeleteImageTemplateAsync(id); 
    }         
}