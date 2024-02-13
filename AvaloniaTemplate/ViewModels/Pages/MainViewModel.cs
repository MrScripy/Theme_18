using AvaloniaTemplate.Services.NavigationService;
using AvaloniaTemplate.Stores;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaTemplate.ViewModels.Pages;

public partial class MainViewModel : ViewModelBase
{
    public string Greeting => "Fuck you, Avalonia!";
    private INavigationService _navigationService;


    public MainViewModel() { }

    public MainViewModel(NavigationService<NavigationStore, AnotherPageViewModel> navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    private void Navigate()
    {
        _navigationService.Navigate();
    }
    
}
