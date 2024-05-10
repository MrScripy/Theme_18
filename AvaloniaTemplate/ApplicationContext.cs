using AvaloniaTemplate.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AvaloniaTemplate.Desktop.AppContext
{
    public class ApplicationContext : DbContext
    {
        public DbSet<AnimalType> AnimalTypes { get; set; } = null!;
        public DbSet<Amphibian> Amphibians { get; set; } = null!;
        public DbSet<Bird> Birds { get; set; } = null!;
        public DbSet<Mammal> Mammals { get; set; } = null!;

        
        public ApplicationContext()
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            //var derictory = AppDomain.CurrentDomain.BaseDirectory;
            optionsBuilder.UseSqlite($"Data Source=animals.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
