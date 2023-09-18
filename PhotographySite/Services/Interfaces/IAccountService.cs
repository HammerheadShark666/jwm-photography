using Microsoft.AspNetCore.Identity;
using static PhotographySite.Helpers.Enums;

namespace PhotographySite.Services.Interfaces;

public interface IAccountService
{
    Task<IdentityResult> RegisterAsync(string email, string password);
    Task<Tuple<Microsoft.AspNetCore.Identity.SignInResult, Role>> LoginAsync(string email, string password);
    Task LogOffAsync();
}