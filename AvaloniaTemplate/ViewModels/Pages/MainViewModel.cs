using AvaloniaTemplate.Desktop.AppContext;
using AvaloniaTemplate.Models;
using AvaloniaTemplate.Models.Factory;
using AvaloniaTemplate.Services.DialogService;
using AvaloniaTemplate.Services.NavigationService;
using AvaloniaTemplate.Stores;
using AvaloniaTemplate.ViewModels.Dialogs.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AvaloniaTemplate.ViewModels.Pages;

public partial class MainViewModel : ViewModelBase
{
    private INavigationService _navigationService;
    private IDialogService _dialogService;
    private IDbContextFactory<ApplicationContext> _DBFactory;


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
        IDbContextFactory<ApplicationContext> DBfactory
        )
    {
        _navigationService = navigationService;
        _dialogService = dialogService;
        _DBFactory = DBfactory;

        BindAnimals();

    }

    private void BindAnimals()
    {
        try
        {
            using (var context = _DBFactory.CreateDbContext())
            {
                Amphibians = context.Amphibians.ToList<Amphibian>();
                Birds = context.Birds.ToList<Bird>();
                Mammals = context.Mammals.ToList<Mammal>();                              
            }
        }
        catch (Exception ex)
        {
            Trace.WriteLine($"Problem {ex.Message}");
        }
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
