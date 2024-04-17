using AvaloniaTemplate.Models;
using AvaloniaTemplate.Models.Factory;
using AvaloniaTemplate.Services.DialogService;
using AvaloniaTemplate.Services.NavigationService;
using AvaloniaTemplate.Stores;
using AvaloniaTemplate.ViewModels.Dialogs.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AvaloniaTemplate.ViewModels.Pages;

public partial class MainViewModel : ViewModelBase
{
    private INavigationService _navigationService;
    private IDialogService _dialogService;

    [ObservableProperty]
    private ObservableCollection<Amphibian> _amphibians = new();
    [ObservableProperty]
    private ObservableCollection<Mammal> _mammals = new();
    [ObservableProperty]
    private ObservableCollection<Bird> _birds = new();
    [ObservableProperty]
    private ObservableCollection<Animal> _commonCollection = new();

    private Factory _factory;


    public MainViewModel() { }

    public MainViewModel(NavigationService<NavigationStore, AnotherPageViewModel> navigationService, IDialogService dialogService)
    {
        _navigationService = navigationService;
        _dialogService = dialogService;
        CreateTestData();
    }

    private void CreateTestData()
    {
        for (int i = 0; i < 30; i++)
        {
            _factory = new AmphibianFactory();
            var amphibian = (Amphibian)_factory.Create(i);
            Amphibians.Add(amphibian);
            CommonCollection.Add(amphibian);
        }
        for (int i = 0; i < 30; i++)
        {
            _factory = new BirdFactory();
            var bird = (Bird)_factory.Create(i);
            Birds.Add(bird);
            CommonCollection.Add(bird);
        }
        for (int i = 0; i < 30; i++)
        {
            _factory = new MammalFactory();
            var mammal = (Mammal)_factory.Create(i);
            Mammals.Add(mammal);
            CommonCollection.Add(mammal);
        }

        Trace.WriteLine("creating data is finished");
    }

    [RelayCommand]
    private void Navigate()
    {
        _navigationService.Navigate();
    }

    [RelayCommand]
    private void AddAnimal()
    {
        _dialogService.ShowDialogAsync<bool>(nameof(AddAnimalWindowViewModel));
    }

}
