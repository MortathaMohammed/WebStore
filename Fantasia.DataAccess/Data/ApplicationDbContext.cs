using Fantasia.DataAccess.Entity;
using Fantasia.DataAccess.Entity.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fantasia.DataAccess.Data;
public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Colore> Colores { get; set; }
    public DbSet<Size> Sizes { get; set; }
}