using Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class AppDbContext:IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions dbContextOptions):base(dbContextOptions) { }
    
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    
    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.StockId }));

        builder.Entity<Portfolio>()
            .HasOne(u => u.AppUser)
            .WithMany(p => p.Portfolios)
            .HasForeignKey(f => f.AppUserId);
        
        builder.Entity<Portfolio>()
            .HasOne(u => u.Stock)
            .WithMany(p => p.Portfolios)
            .HasForeignKey(f => f.StockId);
        
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
    }
}