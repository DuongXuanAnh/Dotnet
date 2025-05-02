using Microsoft.EntityFrameworkCore;
using FinsharkClone.Modals;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FinsharkClone.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions options) : base(options){}

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}