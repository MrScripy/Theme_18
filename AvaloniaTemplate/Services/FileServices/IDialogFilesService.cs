using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System.Threading.Tasks;

namespace AvaloniaTemplate.Services.FileServices
{
    public interface IDialogFilesService
    {
        Window Target { get; }

        Task<IStorageFile?> SaveFileAsync();
    }
}