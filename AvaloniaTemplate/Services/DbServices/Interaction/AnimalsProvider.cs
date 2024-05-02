using System.Collections.Generic;
using System.Threading.Tasks;
using AvaloniaTemplate.Models;
using AvaloniaTemplate.Stores.Db;

namespace AvaloniaTemplate.Services.DbServices.Interaction
{
    internal class AnimalsProvider
    {
        private readonly IRepository<Amphibian> _amphibians;
        private readonly IRepository<Bird> _birds;
        private readonly IRepository<Mammal> _mammals;

        public IEnumerable<Amphibian> Amphibians => _amphibians.Items;

        public IEnumerable<Bird> Birds => _birds.Items;
        public IEnumerable<Mammal> Mammals => _mammals.Items;

        public T AddAnimal<T>(T animal)
        {
            throw new System.NotImplementedException();
        }

        public void AddAnimal(string name, string latName, AnimalType type)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> AddAnimalAsync<T>(T animal)
        {
            throw new System.NotImplementedException();
        }

        public Task AddAnimalAsync(string name, string latName, string type)
        {
            throw new System.NotImplementedException();
        }

        public AnimalsProvider(
            IRepository<Amphibian> amphibians,
            IRepository<Bird> birds,
            IRepository<Mammal> mammals
            )
        {
            this._amphibians = amphibians;
            this._birds = birds;
            this._mammals = mammals;
        }
    }
}
