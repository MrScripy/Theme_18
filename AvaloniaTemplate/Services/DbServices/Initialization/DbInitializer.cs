using AvaloniaTemplate.Desktop.AppContext;
using AvaloniaTemplate.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
                await db.Database.EnsureDeletedAsync().ConfigureAwait(false);

                Task? dbCreate = db?.Database.MigrateAsync();

                if (!await db.AnimalTypes.AnyAsync<AnimalType>())
                {
                    dbCreate?.Wait();

                    if (await db.Database.CanConnectAsync())
                    {
                        var amT = new AnimalType() { Name = "Amphibians" };
                        var bT = new AnimalType() { Name = "Birds" };
                        var mT = new AnimalType() { Name = "Mammals" };

                        List<Amphibian> amphibians = new()
                        {
                             new Amphibian()
                            {
                                Name = "A",
                                LatName = "A",
                                AnimalType = amT
                            },
                              new Amphibian()
                            {
                                Name = "Avv",
                                LatName = "Avv",
                                AnimalType = amT
                            },
                        };

                        List<Bird> birds = new()
                        {
                             new Bird()
                            {
                                Name = "sss",
                                LatName = "ssA",
                                AnimalType = bT
                            },
                              new Bird()
                            {
                                Name = "Assvv",
                                LatName = "ssAvv",
                                AnimalType = bT
                            },
                        };

                        List<Mammal> mammals = new()
                        {
                            new Mammal()
                        {
                            Name = "M",
                            LatName = "M",
                            AnimalType = mT
                        },
                            new Mammal()
                        {
                            Name = "Ms",
                            LatName = "Mss",
                            AnimalType = mT
                        }
                        };

                        await db.AnimalTypes.AddRangeAsync(amT, bT, mT);

                        await db.Amphibians.AddRangeAsync(amphibians);

                        await db.Birds.AddRangeAsync(birds);

                        await db.Mammals.AddRangeAsync(mammals);

                        await db.SaveChangesAsync();

                    }
                }
            }

        }
    }
}
