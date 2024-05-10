using System;
using AvaloniaTemplate.Models;
using AvaloniaTemplate.Stores.Db;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;


namespace AvaloniaTemplate.Services.DbServices.Interaction
{
    public class AnimalsProvider<T> : IAnimalsProvider<T> where T : class, IAnimal, new()
    {
        private readonly IRepository<T> _animals;
        private readonly IRepository<AnimalType> _animalTypes;

        public IQueryable<T> Animals => _animals.Items;
        public T AddAnimal(T animal)
        {
            if (animal == null)
                throw new Exception($"{animal} can not be null");
            return _animals.Add(animal);
        }
        public async Task<T> AddAnimalAsync(T animal)
        {
            if (animal == null)
                throw new Exception($"{animal} can not be null");
            return await _animals.AddAsync(animal);
        }

        public async Task<T> AddAnimalAsync(string name, string latName)
        {
            if (name == null || latName == null) throw new Exception("name or lat name can not be null");
            string animalTypeName = typeof(T).Name + "s";

            var animalType = await _animalTypes.Items.FirstOrDefaultAsync(t => t.Name == animalTypeName);

            if (animalType == null) throw new Exception("there is no a such animal type in DB");

            T newAnimal = new T()
            {
                Name = name,
                LatName = latName,
                AnimalType = animalType
            };
            return await _animals.AddAsync(newAnimal);
        }
        public T AddAnimal(string name, string latName)
        {
            if (name == null || latName == null) throw new Exception("name or lat name can not be null");
            string animalTypeName = typeof(T).Name + "s";

            var animalType = _animalTypes.Items.FirstOrDefault(t => t.Name == "Amphibians");

            if (animalType == null) throw new Exception("there is no a such animal type in DB");

            T newAnimal = new T()
            {
                Name = name,
                LatName = latName,
                AnimalType = animalType
            };
            return _animals.Add(newAnimal);
        }

        public T RemoveAnimal(T animal)
        {
            if (animal == null)
                throw new Exception($"{animal} can not be null");
            _animals.Remove(animal.Id);
            return animal;
        }
        public async Task<T> RemoveAnimalAsync(T animal)
        {
            if (animal == null)
                throw new Exception($"{animal} can not be null");
            await _animals.RemoveAsync(animal.Id);
            return animal;
        }

        public T UpdateAnimal(T animal)
        {
            if (animal == null)
                throw new Exception($"{animal} can not be null");
            _animals.Update(animal);
            return animal;
        }
        public async Task<T> UpdateAnimalAsync(T animal)
        {
            if (animal == null)
                throw new Exception($"{animal} can not be null");
            await _animals.UpdateAsync(animal);
            return animal;
        }


        public AnimalsProvider(
            IRepository<T> animals,
            IRepository<AnimalType> animalTypes
            )
        {
            _animals = animals;
            _animalTypes = animalTypes;
        }
    }
}
