using System.ComponentModel.DataAnnotations;

namespace PhotographySite.Dto.Request;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email Address")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
