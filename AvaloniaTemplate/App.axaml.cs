using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvaloniaTemplate.Desktop.AppContext;
using AvaloniaTemplate.Models;
using AvaloniaTemplate.Services.DbServices.Initialization;
using AvaloniaTemplate.Services.DbServices.Interaction;
using AvaloniaTemplate.Services.DialogService;
using AvaloniaTemplate.Services.FileServices;
using AvaloniaTemplate.Services.NavigationService;
using AvaloniaTemplate.Stores;
using AvaloniaTemplate.Stores.Db;
using AvaloniaTemplate.ViewModels;
using AvaloniaTemplate.ViewModels.Dialogs.Pages;
using AvaloniaTemplate.ViewModels.Pages;
using AvaloniaTemplate.Views;
using AvaloniaTemplate.Views.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;
using System;

namespace AvaloniaTemplate;

public partial class App : Application
{
    public IServiceProvider? Services { get; private set; }
    public static new App? Current => Application.Current as App;


    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        InitializeServices();
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        using (var scope = Services?.CreateScope())
        {
            var dbInitializer = scope?.ServiceProvider.GetRequiredService<IDbInitializer>();
            if (dbInitializer != null)
                dbInitializer.InitializeAsync().Wait();
        }


        Window? mainWindow = Services?.GetRequiredService<MainWindow>();
        INavigationService? navigationService =
           Services?.GetRequiredService<NavigationService<NavigationStore, MainViewModel>>();
        navigationService?.Navigate();


        BindingPlugins.DataValidators.RemoveAt(0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = mainWindow;
            desktop.MainWindow?.Show();
        }

        if (mainWindow != null)
            mainWindow.Closing += MainWindow_OnClosing;
        base.OnFrameworkInitializationCompleted();
    }




    private void InitializeServices()
    {
        var host = Host
            .CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.UseMicrosoftDependencyResolver();
                var resolver = Locator.CurrentMutable;
                resolver.InitializeSplat();

                ConfigureServices(services);
            })
            .UseEnvironment(Environments.Development)
            .Build();


        Services = host.Services;
        Services.UseMicrosoftDependencyResolver();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Db
        services.AddDbContextFactory<ApplicationContext>();
        services.AddTransient<IDbInitializer, DbInitializer>();
        services.AddTransient<IRepository<AnimalType>, DbRepository<AnimalType>>();
        services.AddTransient<IRepository<Amphibian>, DbRepository<Amphibian>>();
        services.AddTransient<IRepository<Bird>, DbRepository<Bird>>();
        services.AddTransient<IRepository<Mammal>, DbRepository<Mammal>>();

        services.AddTransient<IAnimalsProvider<Amphibian>, AnimalsProvider<Amphibian>>();
        services.AddTransient<IAnimalsProvider<Bird>, AnimalsProvider<Bird>>();
        services.AddTransient<IAnimalsProvider<Mammal>, AnimalsProvider<Mammal>>();

        // services
        services.AddSingleton<NavigationStore>();

        services.AddSingleton<NavigationService<NavigationStore, MainViewModel>>();
        services.AddSingleton<Func<MainViewModel>>(s => () => s.GetRequiredService<MainViewModel>());

        services.AddSingleton<IDialogService, DialogService>();
        services.AddSingleton<IDialogFilesService, DialogFilesService>();
        services.AddSingleton<IFilesProvider, FilesProvider>();

        // view models
        services.AddTransient<MainViewModel>();
        services.AddTransient<AddAnimalWindowViewModel>();
        services.AddTransient<ChangeAnimalDialogWindowViewModel>();
        services.AddSingleton<MainWindowViewModel>();

        // main window
        services.AddSingleton(s => new MainWindow()
        {
            DataContext = s.GetRequiredService<MainWindowViewModel>()
        });
    }
    private void MainWindow_OnClosing(object? sender, WindowClosingEventArgs e)
    {
        var window = sender as Window;
        if (window != null)
        {
            window.Closing -= MainWindow_OnClosing;
        }
    }
       
}
