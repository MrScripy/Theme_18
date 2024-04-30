using AvaloniaTemplate.Desktop.AppContext;
using AvaloniaTemplate.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
                    //Task<List<AnimalType>> typesCreate = CreateAnimalTypes();
                    //Task.WaitAll(dbCreate, typesCreate);



                    if (await db.Database.CanConnectAsync())
                    {
                        var amT = new AnimalType() { Name = "Amphibians" };
                        var bT = new AnimalType() { Name = "Birds" };
                        var mT = new AnimalType() { Name = "Mammals" };

                        await db.AnimalTypes.AddRangeAsync(amT, bT, mT);

                        await db.Amphibians.AddAsync(
                            new Amphibian()
                            {
                                Name = "A",
                                LatName = "A",
                                AnimalType = amT
                            });
                        await db.Birds.AddAsync(new Bird()
                        {
                            Name = "B",
                            LatName = "B",
                            AnimalType = bT
                        });
                        await db.Mammals.AddAsync(new Mammal()
                        {
                            Name = "M",
                            LatName = "M",
                            AnimalType = mT
                        });
                        await db.SaveChangesAsync();

                    }

                    await db.SaveChangesAsync();
                }
            }

        }
    }
}
