using AvaloniaTemplate.Desktop.AppContext;
using AvaloniaTemplate.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AvaloniaTemplate.Services.DbServices.Initialization
{
    public class DbInitializer : IDbInitializer
    {
        public DbInitializer(IDbContextFactory<ApplicationContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        private IDbContextFactory<ApplicationContext> _contextFactory { get; }

        public async Task InitializeAsync()
        {
            using (var db = _contextFactory.CreateDbContext())
            {
                if (db == null) return;

                Task? dbCreate = db?.Database.MigrateAsync();

                if (await db.AnimalTypes.AnyAsync<AnimalType>()) return;

                dbCreate?.Wait();

                if (await db.Database.CanConnectAsync())
                {
                    var amT = new AnimalType() { Name = "Amphibians" };
                    var bT = new AnimalType() { Name = "Birds" };
                    var mT = new AnimalType() { Name = "Mammals" };

                    await db.AnimalTypes.AddRangeAsync(amT, bT, mT);
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}
