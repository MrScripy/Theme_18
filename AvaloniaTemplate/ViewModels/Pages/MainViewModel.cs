using AvaloniaTemplate.Models;
using AvaloniaTemplate.Models.Factory;
using AvaloniaTemplate.Services.DialogService;
using AvaloniaTemplate.Services.NavigationService;
using AvaloniaTemplate.Stores;
using AvaloniaTemplate.Stores.Db;
using AvaloniaTemplate.ViewModels.Dialogs.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Linq;

namespace AvaloniaTemplate.ViewModels.Pages;

public partial class MainViewModel : ViewModelBase
{
    private INavigationService _navigationService;
    private IDialogService _dialogService;
    private readonly IRepository<Mammal> _mammalsRepository;
    private readonly IRepository<Amphibian> _amphibiansRepository;
    private readonly IRepository<Bird> _birdsRepository;
    [ObservableProperty]
    private List<Amphibian> _amphibians = new();
    [ObservableProperty]
    private List<Mammal> _mammals = new();
    [ObservableProperty]
    private List<Bird> _birds = new();

    private Factory _factory;


    public MainViewModel() { }

    public MainViewModel(
        NavigationService<NavigationStore, AnotherPageViewModel> navigationService,
        IDialogService dialogService,
        IRepository<Mammal> mammalsRepository,
        IRepository<Amphibian> amphibiansRepository,
        IRepository<Bird> birdsRepository
        )
    {
        _navigationService = navigationService;
        _dialogService = dialogService;
        _mammalsRepository = mammalsRepository;
        _amphibiansRepository = amphibiansRepository;
        _birdsRepository = birdsRepository;
        Mammals = _mammalsRepository.Items.ToList();
        Amphibians = _amphibiansRepository.Items.ToList();
        Birds = _birdsRepository.Items.ToList();

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
