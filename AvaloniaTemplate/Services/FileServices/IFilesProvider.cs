using AvaloniaTemplate.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvaloniaTemplate.Services.FileServices
{
    public interface IFilesProvider
    {
        Task SaveReportAsync(IEnumerable<Animal> animals);
    }
}