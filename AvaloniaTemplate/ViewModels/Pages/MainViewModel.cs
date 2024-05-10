using AvaloniaTemplate.Desktop.AppContext;
using AvaloniaTemplate.Models;
using AvaloniaTemplate.Models.Factory;
using AvaloniaTemplate.Services.DbServices.Interaction;
using AvaloniaTemplate.Services.DialogService;
using AvaloniaTemplate.Services.FileServices;
using AvaloniaTemplate.Services.NavigationService;
using AvaloniaTemplate.Stores;
using AvaloniaTemplate.Stores.Db;
using AvaloniaTemplate.ViewModels.Dialogs.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AvaloniaTemplate.ViewModels.Pages;

public partial class MainViewModel : ViewModelBase
{
    private INavigationService _navigationService;
    private IDialogService _dialogService;
    private readonly IRepository<AnimalType> _animalTypeRepository;
    private readonly IAnimalsProvider<Amphibian> _amphibianProvider;
    private readonly IAnimalsProvider<Bird> _birdProvider;
    private readonly IAnimalsProvider<Mammal> _mammalProvider;
    private readonly IFilesProvider _filesProvider;
    [ObservableProperty]
    private ObservableCollection<Amphibian> _amphibians = new();
    [ObservableProperty]
    private ObservableCollection<Mammal> _mammals = new();
    [ObservableProperty]
    private ObservableCollection<Bird> _birds = new();
    [ObservableProperty]
    private int _selectedTabItem;

    private Factory _factory;

    public IDbContextFactory<ApplicationContext> ContextFactory { get; }

    public MainViewModel() { }

    public MainViewModel(
        NavigationService<NavigationStore, AnotherPageViewModel> navigationService,
        IDialogService dialogService,
        IRepository<AnimalType> animalTypeRepository,
        IAnimalsProvider<Amphibian> amphibianProvider,
        IAnimalsProvider<Bird> birdProvider,
        IAnimalsProvider<Mammal> mammalProvider,
        IDbContextFactory<ApplicationContext> contextFactory,
        IFilesProvider filesProvider
        )
    {
        _navigationService = navigationService;
        _dialogService = dialogService;

        _animalTypeRepository = animalTypeRepository;
        _amphibianProvider = amphibianProvider;
        _birdProvider = birdProvider;
        _mammalProvider = mammalProvider;

        ContextFactory = contextFactory;
        _filesProvider = filesProvider;
    }

    [RelayCommand]
    private void Navigate()
    {
        _navigationService.Navigate();
    }

    [RelayCommand]
    private async Task AddAnimal()
    {
        var animal = await _dialogService.ShowDialogAsync<Animal>(nameof(AddAnimalWindowViewModel));
        if (animal == null) return;


        using (var db = ContextFactory.CreateDbContext())
        {

            // var animalType = await db.AnimalTypes.FirstOrDefaultAsync(t => t.Name == animal.GetType().Name + "s");
            var animalType = animal.GetType().Name;
            switch (animalType)
            {
                case "Amphibian":
                    var newAmphibian = animal as Amphibian;
                    Amphibians.Add(newAmphibian);
                    await _amphibianProvider.AddAnimalAsync(newAmphibian);
                    break;
                case "Bird":
                    var newBird = animal as Bird;
                    Birds.Add(newBird);
                    await _birdProvider.AddAnimalAsync(newBird);
                    break;
                case "Mammal":
                    var newMammal = animal as Mammal;
                    Mammals.Add(newMammal);
                    await _mammalProvider.AddAnimalAsync(newMammal);
                    break;
                default:
                    break;
            }
        }
    }

    [RelayCommand]
    private async Task LoadData()
    {
        foreach (var animal in _mammalProvider.Animals)
            Mammals.Add(animal);

        foreach (var animal in _amphibianProvider.Animals)
            Amphibians.Add(animal);

        foreach (var animal in _birdProvider.Animals)
            Birds.Add(animal);
    }

    [RelayCommand]
    private async Task Save()
    {
        switch (SelectedTabItem)
        {
            case 0:
                await _filesProvider.SaveReportAsync(Mammals);
                break;
            case 1:
                await _filesProvider.SaveReportAsync(Birds);
                break;
            case 2:
                await _filesProvider.SaveReportAsync(Amphibians);
                break;
            default:
                break;
        }

    }
}


