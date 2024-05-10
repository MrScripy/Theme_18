using AvaloniaTemplate.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaTemplate.ViewModels.Dialogs.Pages
{
    public partial class ChangeAnimalDialogWindowViewModel : ParamDialogViewModelBase<Animal, Animal>
    {
        private Animal _animal;
        [ObservableProperty]
        private string _name;
        [ObservableProperty]
        private string _latName;

        public override void Activate(Animal param)
        {
            _animal = param;
            Name = param.Name;
            LatName = param.LatName;
        }

        public override async Task ActivateAsync(Animal param, CancellationToken token = default)
        {
            _animal = param;
            Name = param.Name;
            LatName = param.LatName;
            await Task.Yield();
        }

        [RelayCommand]
        private void ChangeAnimal()
        {
            _animal.Name = Name;
            _animal.LatName = LatName;
            Close(_animal);
        }
    }
}
