using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Services.Interfaces;
using PhotographySite.ViewModels;
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
    public async Task<ActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        else
        {
            IdentityResult result = await _accountService.RegisterAsync(model.Email, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }
    }

    [HttpGet("login")]
    public ActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        else
        {
            var response = await _accountService.LoginAsync(model.Email, model.Password);

            if(response.Item1.Succeeded)
            {
                if(response.Item2 == Role.Admin)
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                else
                    return RedirectToAction("Index", "Home", new { area = "Site" });
            }
            else
            {
                ModelState.AddModelError("", "There is something wrong with your email or username. Please try again.");
                return View(model);
            }
        }
    }

    [HttpPost("logoff")]
    public async Task<ActionResult> LogOff()
    {
        await _accountService.LogOffAsync();
        return RedirectToAction("Login");
    }
      
    [HttpGet("AccessDenied")]
    public IActionResult AccessDenied()
    {
        return View();
    }
}