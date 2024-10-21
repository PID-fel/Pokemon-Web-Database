using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BulbaClone.Models;

namespace BulbaClone.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BulbaClone.Models.PkmnType> PkmnType { get; set; } = default!;
        public DbSet<BulbaClone.Models.Form> Form { get; set; } = default!;
        public DbSet<BulbaClone.Models.Pokemon> Pokemon { get; set; } = default!;
    }
}
