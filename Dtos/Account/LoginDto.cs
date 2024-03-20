using Microsoft.Build.Framework;

namespace Api.Dtos.Account;

public class LoginDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}