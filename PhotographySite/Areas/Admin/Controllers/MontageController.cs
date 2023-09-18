using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Dto.Request;

namespace PhotographySite.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
[Route("admin/montage")]
[AutoValidateAntiforgeryToken]
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
	public async Task<IActionResult> AddMontageImageAsync([FromBody] MontageRequest montageRequest)
    {
        return Ok(await _montageService.AddImageTemplateAsync(montageRequest.Column, montageRequest.Order, montageRequest.Orientation));
    }

    [HttpPost("move-image")] 
	public async Task MoveMontageImageAsync([FromBody] MontageRequest montageRequest)
    {
        await _montageService.MoveImageTemplateAsync(montageRequest.Id, montageRequest.Column, montageRequest.Order); 
    }

    [HttpDelete("delete-image/{id:int}")]
    public async Task DeleteMontageImageAsync(int id)
    {
        await _montageService.DeleteImageTemplateAsync(id); 
    }         
}