using FinsharkClone.Modals;

namespace FinsharkClone.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}