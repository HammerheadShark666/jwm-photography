using Microsoft.AspNetCore.Identity;
using PhotographySite.Models;
using PhotographySite.Services.Interfaces;
using static PhotographySite.Helpers.Enums;

namespace PhotographySite.Services;

public class AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager) : IAccountService
{
    public async Task<IdentityResult> RegisterAsync(string email, string password)
    {
        ApplicationUser user = new() { UserName = email, Email = email };

        IdentityResult result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            var defaultrole = roleManager.FindByNameAsync("User").Result;
            if (defaultrole != null)
            {
                await userManager.AddToRoleAsync(user, defaultrole.Name);
            }
        }

        return result;
    }

    public async Task<Tuple<Microsoft.AspNetCore.Identity.SignInResult, Role>> LoginAsync(string email, string password)
    {
        Role role = Role.User;

        SignInResult result = await signInManager.PasswordSignInAsync(email, password, isPersistent: true, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            var user = await userManager.FindByNameAsync(email);
            if (user != null)
            {
                List<string> roles = (List<string>)await userManager.GetRolesAsync(user);

                if (roles.Contains("Admin"))
                    role = Role.Admin;
            }
        }

        return new Tuple<SignInResult, Role>(result, role);
    }

    public async Task LogOffAsync()
    {
        await signInManager.SignOutAsync();
    }
}
