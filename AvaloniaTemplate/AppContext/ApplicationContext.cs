using Microsoft.EntityFrameworkCore;

namespace AvaloniaTemplate.AppContext
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
            Database.Migrate();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
