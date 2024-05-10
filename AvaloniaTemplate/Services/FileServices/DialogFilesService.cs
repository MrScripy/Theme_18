using Avalonia.Controls;
using Avalonia.Platform.Storage;
using AvaloniaTemplate.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace AvaloniaTemplate.Services.FileServices
{
    public class DialogFilesService : IDialogFilesService
    {
        private Window _target;
        public Window Target => _target ??= App.Current.Services.GetRequiredService<MainWindow>();

        public async Task<IStorageFile?> SaveFileAsync()
        {
            return await Target.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions()
            {
                Title = "Save File",
                SuggestedFileName = "Report.txt",
                DefaultExtension = "txt",
                ShowOverwritePrompt = true
            });
        }
    }
}
