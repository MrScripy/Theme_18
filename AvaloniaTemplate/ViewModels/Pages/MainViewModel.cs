using AvaloniaTemplate.Desktop.AppContext;
using AvaloniaTemplate.Models;
using AvaloniaTemplate.Models.Factory;
using AvaloniaTemplate.Services.DbServices.Interaction;
using AvaloniaTemplate.Services.DialogService;
using AvaloniaTemplate.Services.FileServices;
using AvaloniaTemplate.Stores.Db;
using AvaloniaTemplate.ViewModels.Dialogs.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AvaloniaTemplate.ViewModels.Pages;

public partial class MainViewModel : ViewModelBase
{
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

    [NotifyCanExecuteChangedFor(nameof(RemoveAnimalCommand))]
    [NotifyCanExecuteChangedFor(nameof(ChangeAnimalCommand))]
    [NotifyCanExecuteChangedFor(nameof(ChangeAnimalCommand))]
    [ObservableProperty]
    private Animal _selectedAnimal;

    private Factory _factory;

    public IDbContextFactory<ApplicationContext> ContextFactory { get; }

    public MainViewModel() { }

    public MainViewModel(
        IDialogService dialogService,
        IRepository<AnimalType> animalTypeRepository,
        IAnimalsProvider<Amphibian> amphibianProvider,
        IAnimalsProvider<Bird> birdProvider,
        IAnimalsProvider<Mammal> mammalProvider,
        IDbContextFactory<ApplicationContext> contextFactory,
        IFilesProvider filesProvider
        )
    {
        _dialogService = dialogService;

        _animalTypeRepository = animalTypeRepository;
        _amphibianProvider = amphibianProvider;
        _birdProvider = birdProvider;
        _mammalProvider = mammalProvider;

        ContextFactory = contextFactory;
        _filesProvider = filesProvider;
    }

    [RelayCommand]
    private async Task AddAnimalAsync()
    {
        var animal = await _dialogService.ShowDialogAsync<Animal>(nameof(AddAnimalWindowViewModel));
        if (animal == null) return;

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

    [RelayCommand]
    private void LoadData()
    {
        foreach (var animal in _mammalProvider.Animals)
            Mammals.Add(animal);

        foreach (var animal in _amphibianProvider.Animals)
            Amphibians.Add(animal);

        foreach (var animal in _birdProvider.Animals)
            Birds.Add(animal);
    }

    [RelayCommand]
    private async Task SaveAsync()
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

    [RelayCommand(CanExecute = nameof(CanChangeOrRemoveAnimal))]
    private async Task RemoveAnimalAsync()
    {
        var animalType = SelectedAnimal.GetType().Name;
        switch (animalType)
        {
            case "Amphibian":
                var newAmphibian = SelectedAnimal as Amphibian;
                Amphibians.Remove(newAmphibian);
                await _amphibianProvider.RemoveAnimalAsync(newAmphibian);
                break;
            case "Bird":
                var newBird = SelectedAnimal as Bird;
                Birds.Remove(newBird);
                await _birdProvider.RemoveAnimalAsync(newBird);
                break;
            case "Mammal":
                var newMammal = SelectedAnimal as Mammal;
                Mammals.Remove(newMammal);
                await _mammalProvider.RemoveAnimalAsync(newMammal);
                break;
            default:
                break;
        }
    }
    [RelayCommand(CanExecute = nameof(CanChangeOrRemoveAnimal))]
    private async Task ChangeAnimalAsync()
    {
        var resultAnimal = await _dialogService.ShowDialogAsync<Animal, Animal>(
            nameof(ChangeAnimalDialogWindowViewModel), SelectedAnimal);

        var animalType = resultAnimal.GetType().Name;
        switch (animalType)
        {
            case "Amphibian":
                Amphibians = new();
                var newAmphibian = resultAnimal as Amphibian;
                await _amphibianProvider.UpdateAnimalAsync(newAmphibian);
                foreach (var animal in _amphibianProvider.Animals)
                    Amphibians.Add(animal);
                break;
            case "Bird":
                Birds = new();
                var newBird = resultAnimal as Bird;
                await _birdProvider.UpdateAnimalAsync(newBird);
                foreach (var animal in _birdProvider.Animals)
                    Birds.Add(animal);
                break;
            case "Mammal":
                Mammals = new();
                var newMammal = resultAnimal as Mammal;
                await _mammalProvider.UpdateAnimalAsync(newMammal);
                foreach (var animal in _mammalProvider.Animals)
                    Mammals.Add(animal);
                break;
            default:
                break;
        }

    }
    private bool CanChangeOrRemoveAnimal() => SelectedAnimal == null ? false : true;
}


