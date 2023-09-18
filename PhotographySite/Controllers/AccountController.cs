using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Dto.Request;
using PhotographySite.Services.Interfaces;
using static PhotographySite.Helpers.Enums;

namespace PhotographySite.Controllers;

[Route("")]
public class AccountController : Controller
{   
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
  
    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterRequest registerRequest)
    {
        if (!ModelState.IsValid) 
            return View(registerRequest); 
        else
        {
            IdentityResult result = await _accountService.RegisterAsync(registerRequest.Email, registerRequest.Password);

            if (result.Succeeded) 
                return RedirectToAction("Login", "Account"); 
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(registerRequest);
            }
        }
    }

    [HttpGet("login")]
    public ActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginRequest loginRequest)
    {
        if (!ModelState.IsValid) 
            return View(loginRequest); 
        else
        {
            var response = await _accountService.LoginAsync(loginRequest.Email, loginRequest.Password);
             
            if(response.Item1.Succeeded)
            { 
                return RedirectToAction("Index", "Home", new { area = (response.Item2 == Role.Admin ? "Admin" : "Site") });
            }
            else
            {
                ModelState.AddModelError("", "There is something wrong with your email or username. Please try again.");
                return View(loginRequest);
            }
        }
    }

    [HttpPost("logoff")]
    public async Task<ActionResult> LogOff()
    {
        await _accountService.LogOffAsync();
        return RedirectToAction("Index", "Home", new { area = "Site" });
    }
      
    [HttpGet("AccessDenied")]
    public IActionResult AccessDenied()
    {
        return View();
    }
}