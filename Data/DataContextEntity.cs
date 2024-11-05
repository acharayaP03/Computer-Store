
using ComputerStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ComputerStore.Data;
public class DataContextEntity : DbContext
{

    public DbSet<Computer>? Computer { get; set; }

    private readonly IConfiguration _configuration;

    public DataContextEntity(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"), (options) => {
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

