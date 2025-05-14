using Microsoft.AspNetCore.Identity;

namespace FinsharkClone.Modals
{
    public class AppUser : IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}