using kol_2.Models;
using Microsoft.EntityFrameworkCore;

namespace kol_2.Data;

public class SeedingDbContext : DbContext
{
    
    public DbSet<Nursery> Nurseries { get; set; }
    public DbSet<TreeSpecies> TreeSpecies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<SeedlingBatch> SeedlingBatches { get; set; }
    public DbSet<Responsible> Responsibles { get; set; }
    
    protected SeedingDbContext()
    {
    }

    public SeedingDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Nursery>().HasData(new List<Nursery>()
        {
            new Nursery() { NurseryId = 1, Name = "First", EstablishedDate = DateTime.Parse("2025-06-25")},
            new Nursery() { NurseryId = 2, Name = "Second", EstablishedDate = DateTime.Parse("2025-06-27")}
        });

        modelBuilder.Entity<TreeSpecies>().HasData(new List<TreeSpecies>()
        {
            new TreeSpecies() { SpeciesId = 1, LatinName = "abc", GrowthTimeLnYears = 5},
            new TreeSpecies() { SpeciesId = 2, LatinName = "def", GrowthTimeLnYears = 6}
        });

        modelBuilder.Entity<Employee>().HasData(new List<Employee>()
        {
            new Employee() { EmployeeId = 1, FirstName = "John", LastName = "Doe", HireDate = DateTime.Parse("2025-06-25") },
            new Employee() { EmployeeId = 2, FirstName = "Jenny", LastName = "Doe", HireDate = DateTime.Parse("2025-06-27") }
        });
        
        modelBuilder.Entity<SeedlingBatch>().HasData(new List<SeedlingBatch>()
        {
            new SeedlingBatch() { BatchId = 1, NurseryId = 1, SpeciesId = 1, Quantity = 1, SownDate = DateTime.Parse("2025-06-25"),ReadyDate = null},
            new SeedlingBatch() { BatchId = 2, NurseryId = 2, SpeciesId = 2, Quantity = 2, SownDate = DateTime.Parse("2025-06-27"), ReadyDate = null}
        });

        modelBuilder.Entity<Responsible>().HasData(new List<Responsible>()
        {
            new Responsible() { BatchId = 1, EmployeeId = 1, Role = "Gardener"},
            new Responsible() { BatchId = 2, EmployeeId = 2, Role = "Gardener" },
        });

    }
}