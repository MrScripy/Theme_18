using AvaloniaTemplate.Desktop.AppContext;
using AvaloniaTemplate.Models;
using AvaloniaTemplate.Models.Factory;
using AvaloniaTemplate.Services.DbServices.Interaction;
using AvaloniaTemplate.Services.DialogService;
using AvaloniaTemplate.Services.NavigationService;
using AvaloniaTemplate.Stores;
using AvaloniaTemplate.Stores.Db;
using AvaloniaTemplate.ViewModels.Dialogs.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    [ObservableProperty]
    private List<Amphibian> _amphibians = new();
    [ObservableProperty]
    private List<Mammal> _mammals = new();
    [ObservableProperty]
    private List<Bird> _birds = new();

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
        IDbContextFactory<ApplicationContext> contextFactory
        )
    {
        _navigationService = navigationService;
        _dialogService = dialogService;

        _animalTypeRepository = animalTypeRepository;
        _amphibianProvider = amphibianProvider;
        _birdProvider = birdProvider;
        _mammalProvider = mammalProvider;

        ContextFactory = contextFactory;

        //Mammals = _mammalsRepository.Items.ToList();
        //Amphibians = _amphibiansRepository.Items.ToList();
        //Birds = _birdsRepository.Items.ToList();

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
                    await _amphibianProvider.AddAnimalAsync(animal as Amphibian);
                    break;
                case "Bird":
                    await _birdProvider.AddAnimalAsync(animal as Bird);
                    break;
                case "Mammal":
                    await _mammalProvider.AddAnimalAsync(animal as Mammal);
                    break;
                default:
                    break;
            }
        }
    }
}


