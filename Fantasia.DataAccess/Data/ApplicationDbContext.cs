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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Colore> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }

        // protected override void OnModelCreating(ModelBuilder builder)
        // {
        //         builder.Entity<ProductColor>()
        //                 .HasKey(pc => new { pc.ProductId, pc.ColorId });

        //         builder.Entity<ProductSize>()
        //                 .HasKey(pz => new { pz.ProductId, pz.SizeId });

        //         builder.Entity<ProductColor>()
        //                 .HasOne(p => p.Product)
        //                 .WithMany(c => c.ProductColours)
        //                 .HasForeignKey(p => p.ProductId);

        //         builder.Entity<ProductColor>()
        //                .HasOne(c => c.Colore)
        //                .WithMany(pc => pc.ProductColours)
        //                .HasForeignKey(p => p.ColorId);

        //         builder.Entity<ProductSize>()
        //                 .HasOne(p => p.Product)
        //                 .WithMany(c => c.ProductSizes)
        //                 .HasForeignKey(p => p.ProductId);

        //         builder.Entity<ProductSize>()
        //                .HasOne(c => c.Size)
        //                .WithMany(pc => pc.ProductSizes)
        //                .HasForeignKey(p => p.SizeId);

        // }
}