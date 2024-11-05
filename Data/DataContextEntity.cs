
using ComputerStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Data;


public class DataContextEntity : DbContext
{
    // "Server=localhost;Database=ComputerStore;Trusted_Connection=True;TrustServerCertificate=True;"
    
    public DbSet<Computer>? Computer { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=ComputerStore;Trusted_Connection=True;TrustServerCertificate=True;", (options) => {
                options.EnableRetryOnFailure();
            });
        }
            
    }


    // mapping model Computer to table Computer
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ComputerStoreAppSchema");

        modelBuilder.Entity<Computer>()
            .HasKey(c => c.ComputerId);
            // .ToTable("Computer", "ComputerStoreAppSchema");
    }
}

