using Bulky.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor that takes DbContextOptions and passes it to the base class constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories {  get; set; }
        
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Naam = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Naam = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Naam = "History", DisplayOrder = 3 }
            );
        }
    }
}

