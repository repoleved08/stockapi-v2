using Api.Models;

namespace Api.Interface;

public interface ITokenInterface
{
    Task<string> CreateToken(AppUser user);
}
