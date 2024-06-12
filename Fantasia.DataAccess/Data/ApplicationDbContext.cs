using Fantasia.DataAccess.Entity;
using Fantasia.DataAccess.Entity.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Fantasia.DataAccess.Data;
public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Color> Colours { get; set; }
    public DbSet<Size> Sizes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ProductColor>()
                .HasKey(pc => new { pc.ProductId, pc.ColorId });

        builder.Entity<ProductColor>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductColours)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ProductColor>()
                .HasOne(pc => pc.Color)
                .WithMany(c => c.ProductColours)
                .HasForeignKey(pc => pc.ColorId)
                .OnDelete(DeleteBehavior.Restrict);


        builder.Entity<ProductSize>()
                .HasKey(pz => new { pz.ProductId, pz.SizeId });

        builder.Entity<ProductSize>()
                .HasOne(pz => pz.Product)
                .WithMany(p => p.ProductSizes)
                .HasForeignKey(pz => pz.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ProductSize>()
                .HasOne(pz => pz.Size)
                .WithMany(z => z.ProductSizes)
                .HasForeignKey(pz => pz.SizeId)
                .OnDelete(DeleteBehavior.Restrict);

        List<IdentityRole> roles = new List<IdentityRole>
                {
                new IdentityRole
                {
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                        Name = "User",
                        NormalizedName = "USER"
                }
                };
        builder.Entity<IdentityRole>().HasData(roles);
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
}