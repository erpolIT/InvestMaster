using System.Collections.Immutable;
using backend.DatabaseSeeder;
using backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Database;

public class ApiDbContext : IdentityDbContext<User>
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
        
    }

    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<Investment> Investments { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<AssetType> AssetTypes { get; set; }
    public DbSet<PortfolioValue> PortfolioValues { get; set; }
    public DbSet<AccountBalance> AccountBalances { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.SeedAssets();
        modelBuilder.Entity<User>().Property(u => u.Initials).HasMaxLength(5);
        modelBuilder.HasDefaultSchema("identity"); 
        
        modelBuilder.Entity<User>()
        .HasMany(u => u.Portfolios)
        .WithOne(p => p.User)
        .HasForeignKey(p => p.UserId);



    // Konfiguracja relacji dla Portfolio
    modelBuilder.Entity<Portfolio>()
        .HasMany(p => p.Investments)
        .WithOne(i => i.Portfolio)
        .HasForeignKey(i => i.PortfolioId);


    modelBuilder.Entity<Portfolio>()
        .HasMany(p => p.PortfolioValues)
        .WithOne(pv => pv.Portfolio)
        .HasForeignKey(pv => pv.PortfolioId);
    

    modelBuilder.Entity<Asset>()
        .HasOne(a => a.AssetType)
        .WithMany(at => at.Assets)
        .HasForeignKey(a => a.AssetTypeId);

    modelBuilder.Entity<Asset>()
        .HasMany(a => a.Investments)
        .WithOne(i => i.Asset)
        .HasForeignKey(i => i.AssetId);
    
    
    modelBuilder.Entity<AssetType>()
        .HasMany(a => a.Assets)
        .WithOne(t => t.AssetType)
        .HasForeignKey(t => t.AssetTypeId);

    modelBuilder.Entity<Investment>()
        .HasOne(i => i.Portfolio)
        .WithMany(p => p.Investments)
        .HasForeignKey(i => i.PortfolioId);

    modelBuilder.Entity<Investment>()
        .HasOne(i => i.Asset)
        .WithMany(a => a.Investments)
        .HasForeignKey(i => i.AssetId);
    

    modelBuilder.Entity<PortfolioValue>()
        .HasOne(pv => pv.Portfolio)
        .WithMany(p => p.PortfolioValues)
        .HasForeignKey(pv => pv.PortfolioId);

    modelBuilder.Entity<Transaction>()
        .HasOne(t => t.Investment)  
        .WithMany(i => i.Transactions)  
        .HasForeignKey(t => t.InvestmentId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Investment>()
        .HasMany(i => i.Transactions)
        .WithOne(t => t.Investment)
        .HasForeignKey(t => t.InvestmentId);
    
    modelBuilder.Entity<Portfolio>()
        .HasOne(p => p.AccountBalance)
        .WithOne(a => a.Portfolio)
        .HasForeignKey<AccountBalance>(a => a.PortfolioId);
    }
}