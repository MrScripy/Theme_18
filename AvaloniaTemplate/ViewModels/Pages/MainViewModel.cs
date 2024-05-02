using AvaloniaTemplate.Desktop.AppContext;
using AvaloniaTemplate.Models;
using AvaloniaTemplate.Models.Factory;
using AvaloniaTemplate.Services.DialogService;
using AvaloniaTemplate.Services.NavigationService;
using AvaloniaTemplate.Stores;
using AvaloniaTemplate.Stores.Db;
using AvaloniaTemplate.ViewModels.Dialogs.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvaloniaTemplate.ViewModels.Pages;

public partial class MainViewModel : ViewModelBase
{
    private INavigationService _navigationService;
    private IDialogService _dialogService;
    private readonly IRepository<AnimalType> _animalTypeRepository;
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

    public IDbContextFactory<ApplicationContext> ContextFactory { get; }

    public MainViewModel() { }

    public MainViewModel(
        NavigationService<NavigationStore, AnotherPageViewModel> navigationService,
        IDialogService dialogService,
        IRepository<AnimalType> animalTypeRepository,
        IRepository<Mammal> mammalsRepository,
        IRepository<Amphibian> amphibiansRepository,
        IRepository<Bird> birdsRepository,
        IDbContextFactory<ApplicationContext> contextFactory
        )
    {
        _navigationService = navigationService;
        _dialogService = dialogService;
        _animalTypeRepository = animalTypeRepository;
        _mammalsRepository = mammalsRepository;
        _amphibiansRepository = amphibiansRepository;
        _birdsRepository = birdsRepository;
        ContextFactory = contextFactory;
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
    private async Task AddAnimal()
    {
       // var animal = await _dialogService.ShowDialogAsync<Animal>(nameof(AddAnimalWindowViewModel));
        //if (animal is Amphibian)
        //{
        //    var newAnimal = (Amphibian)animal;            
        //    await _amphibiansRepository.AddAsync(newAnimal);
        //    Amphibians.Add(newAnimal);

        //}
        //else if (animal is Bird)
        //{
        //    await _birdsRepository.AddAsync((Bird)animal);
        //    Birds.Add((Bird)animal);
        //}
        //else if (animal is Mammal)
        //{
        //    await _mammalsRepository.AddAsync((Mammal)animal);
        //    Mammals.Add((Mammal)animal);
        //}
        
        //_amphibiansRepository.Add(test);

        

        //using(var db = ContextFactory.CreateDbContext())
        //{
        //    Amphibian test = new()
        //    {
        //        Name = "Name",
        //        LatName = "LatName",
        //        AnimalType = db.AnimalTypes.FirstOrDefault(t => t.Id == 1),
        //    };
        //    db.Amphibians.Add(test);
        //    db.SaveChanges();
        //}
    }

}
