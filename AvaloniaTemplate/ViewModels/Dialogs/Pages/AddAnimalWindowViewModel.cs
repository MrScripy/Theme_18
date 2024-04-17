using CommunityToolkit.Mvvm.Input;

namespace AvaloniaTemplate.ViewModels.Dialogs.Pages
{
    public partial class AddAnimalWindowViewModel : DialogViewModelBase<bool>
    {
        [RelayCommand]
        private void AddAnimal()
        {
            Close(true);
        }
    }
}
