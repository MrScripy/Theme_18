﻿using AvaloniaTemplate.Models;
using System.Linq;
using System.Threading.Tasks;

namespace AvaloniaTemplate.Services.DbServices.Interaction
{
    public interface IAnimalsProvider<T> where T : class, IAnimal, new()
    {
        IQueryable<T> Animals { get; }

        T AddAnimal(string name, string latName);
        T AddAnimal(T animal);
        T RemoveAnimal(T animal);
        T UpdateAnimal(T animal);
        Task<T> AddAnimalAsync(string name, string latName);
        Task<T> AddAnimalAsync(T animal);
        Task<T> RemoveAnimalAsync(T animal);
        Task<T> UpdateAnimalAsync(T animal);
    }
}