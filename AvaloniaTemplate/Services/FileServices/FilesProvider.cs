using AvaloniaTemplate.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaTemplate.Services.FileServices
{
    public class FilesProvider : IFilesProvider
    {
        private readonly IDialogFilesService _dialogFilesService;

        public FilesProvider(IDialogFilesService dialogFilesService)
        {
            _dialogFilesService = dialogFilesService;
        }

        public async Task SaveReportAsync(IEnumerable<Animal> animals)
        {
            if (animals == null) throw new NullReferenceException();

            var firstAnimal = animals.FirstOrDefault();

            if (firstAnimal == null) return;

            var file = await _dialogFilesService.SaveFileAsync();

            if (file is not null)
            {
                await using var stream = await file.OpenWriteAsync();
                using var streamWriter = new StreamWriter(stream);

                await streamWriter.WriteLineAsync($"{firstAnimal.GetType().Name}s List\n");
                await streamWriter.WriteLineAsync($"name \t lat.name \n");

                var text = new StringBuilder();
                foreach (var animal in animals)
                {
                    text.AppendLine($"{animal.Name} \t {animal.LatName}");
                }
                await streamWriter.WriteLineAsync(text);

            }
        }

    }
}
