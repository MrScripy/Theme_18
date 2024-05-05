using AvaloniaTemplate.Models;
using System.Threading.Tasks;

namespace AvaloniaTemplate.Services.DbServices.Interaction
{
    public interface IAnimalsProvider<T> where T : class, IAnimal, new()
    {
        T AddAnimal(string name, string latName);
        T AddAnimal(T animal);
        Task<T> AddAnimalAsync(string name, string latName);
        Task<T> AddAnimalAsync(T animal);
    }
}