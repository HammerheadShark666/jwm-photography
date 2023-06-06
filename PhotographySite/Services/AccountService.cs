using Microsoft.AspNetCore.Identity;
using PhotographySite.Data.Contexts;
using PhotographySite.Models;
using PhotographySite.Services.Interfaces;
using static PhotographySite.Helpers.Enums;

namespace PhotographySite.Services;

public class AccountService : IAccountService
{
    private readonly PhotographySiteDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private RoleManager<IdentityRole> _roleManager;

    public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, PhotographySiteDbContext db)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _db = db;
    }

    public async Task<IdentityResult> RegisterAsync(string email, string password)
    {
        ApplicationUser user = new ApplicationUser { UserName = email, Email = email };
        IdentityResult result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            var defaultrole = _roleManager.FindByNameAsync("User").Result;

            if (defaultrole != null)
            {
                IdentityResult roleresult = await _userManager.AddToRoleAsync(user, defaultrole.Name);
            }             
        }
        return result;
    }

    public async Task<Tuple<Microsoft.AspNetCore.Identity.SignInResult, Role> > LoginAsync(string email, string password)
    {
        Role role = Role.User;

        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(email, password, isPersistent: true, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(email);

            if(user != null)
            {
                List<string> roles = (List<string>)await _userManager.GetRolesAsync(user);

                if (roles.Contains("Admin"))
                    role = Role.Admin; 
            }            
        } 

        return new Tuple<Microsoft.AspNetCore.Identity.SignInResult, Role>(result, role); 
    }

    public async Task LogOffAsync()
    {
        await _signInManager.SignOutAsync(); 
    }
}
