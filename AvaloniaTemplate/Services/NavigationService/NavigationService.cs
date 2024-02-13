using AvaloniaTemplate.Stores;
using AvaloniaTemplate.ViewModels;
using System;

namespace AvaloniaTemplate.Services.NavigationService;

public class NavigationService<TNavigationStore, TViewModel> : INavigationService
        where TNavigationStore : NavigationStore
        where TViewModel : ViewModelBase
{
    private readonly TNavigationStore _navigationStore;
    private readonly Func<TViewModel> _createViewModel;

    public NavigationService(TNavigationStore navigationStore, Func<TViewModel> createViewModel)
    {
        _navigationStore = navigationStore;
        _createViewModel = createViewModel;
    }

    public void Navigate()
    {
        _navigationStore.CurrentViewModel = _createViewModel();
    }
}