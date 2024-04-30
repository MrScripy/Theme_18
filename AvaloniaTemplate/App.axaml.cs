using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvaloniaTemplate.Desktop.AppContext;
using AvaloniaTemplate.Models;
using AvaloniaTemplate.Services.DialogService;
using AvaloniaTemplate.Services.NavigationService;
using AvaloniaTemplate.Stores;
using AvaloniaTemplate.ViewModels;
using AvaloniaTemplate.ViewModels.Dialogs.Pages;
using AvaloniaTemplate.ViewModels.Pages;
using AvaloniaTemplate.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;

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
        Window? mainWindow = Services?.GetRequiredService<MainWindow>();
        INavigationService? navigationService =
           Services?.GetRequiredService<NavigationService<NavigationStore, MainViewModel>>();
        navigationService?.Navigate();

        await InitDb();

        BindingPlugins.DataValidators.RemoveAt(0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = mainWindow;
            desktop.MainWindow?.Show();
        }

        mainWindow.Closing += MainWindow_OnClosing;
        base.OnFrameworkInitializationCompleted();
    }

    private async Task InitDb()
    {
        using (var db = Services?.GetRequiredService<IDbContextFactory<ApplicationContext>>().CreateDbContext())
        {
            Task? dbCreate = db?.Database.MigrateAsync();
            if (await db.AnimalTypes.FirstOrDefaultAsync<AnimalType>() == null)
            {
                //Task<List<AnimalType>> typesCreate = CreateAnimalTypes();
                //Task.WaitAll(dbCreate, typesCreate);



                if (await db.Database.CanConnectAsync())
                {
                    var amT = new AnimalType() { Name = "Amphibians" };
                    var bT = new AnimalType() { Name = "Birds" };
                    var mT = new AnimalType() { Name = "Mammals" };

                    await db.AnimalTypes.AddRangeAsync(amT, bT, mT);

                    await db.Amphibians.AddAsync(
                        new Amphibian()
                        {
                            Name = "A",
                            LatName = "A",
                            AnimalType = amT
                        });
                    await db.Birds.AddAsync(new Bird()
                    {
                        Name = "B",
                        LatName = "B",
                        AnimalType = bT
                    });
                    await db.Mammals.AddAsync(new Mammal()
                    {
                        Name = "M",
                        LatName = "M",
                        AnimalType = mT
                    });
                    await db.SaveChangesAsync();
                             
                }

                await db.SaveChangesAsync();
            }
        }

        Task<List<AnimalType>> CreateAnimalTypes() => Task.FromResult(new List<AnimalType>
        {
            new AnimalType () { Name = "Amphibians" },
            new AnimalType () { Name = "Birds" },
            new AnimalType() { Name = "Mammals" }
        });


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

        // services
        services.AddSingleton<NavigationStore>();

        services.AddSingleton<NavigationService<NavigationStore, MainViewModel>>();
        services.AddSingleton<Func<MainViewModel>>(s => () => s.GetRequiredService<MainViewModel>());
        services.AddSingleton<NavigationService<NavigationStore, AnotherPageViewModel>>();
        services.AddSingleton<Func<AnotherPageViewModel>>(s => () => s.GetRequiredService<AnotherPageViewModel>());

        services.AddSingleton<IDialogService, DialogService>();

        // view models
        services.AddTransient<FirstDialogWindowViewModel>();

        services.AddTransient<MainViewModel>();
        services.AddTransient<AddAnimalWindowViewModel>();
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
