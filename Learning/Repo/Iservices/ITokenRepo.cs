using Microsoft.AspNetCore.Identity;

namespace Learning.Repo.Iservices
{
    public interface ITokenRepo
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
