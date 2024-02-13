using System.Threading.Tasks;
using AvaloniaTemplate.Services.DialogService;
using AvaloniaTemplate.Services.NavigationService;
using AvaloniaTemplate.Stores;
using AvaloniaTemplate.ViewModels.Dialogs.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaTemplate.ViewModels.Pages
{
    public partial class AnotherPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IDialogService _dialogService;

        [ObservableProperty]
        private string _greeting = "Another page";
        public AnotherPageViewModel() { }
        public AnotherPageViewModel(NavigationService<NavigationStore, MainViewModel> navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        [RelayCommand]
        private void Navigate()
        {
            _navigationService.Navigate();
        }

        [RelayCommand]
        private async Task ShowDialog() 
        {
            var result = await _dialogService.ShowDialogAsync<bool>(nameof(FirstDialogWindowViewModel));
        }
    }
}
