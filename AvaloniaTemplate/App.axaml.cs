using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvaloniaTemplate.Services.DialogService;
using AvaloniaTemplate.Services.NavigationService;
using AvaloniaTemplate.Stores;
using AvaloniaTemplate.ViewModels;
using AvaloniaTemplate.ViewModels.Dialogs.Pages;
using AvaloniaTemplate.ViewModels.Pages;
using AvaloniaTemplate.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;

namespace AvaloniaTemplate;

public partial class App : Application
{
    public IServiceProvider Services { get; private set; }
    public static new App Current => (App)Application.Current;


    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        InitializeServices();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        Window mainWindow = Services.GetRequiredService<MainWindow>();
        INavigationService navigationService =
           Services.GetRequiredService<NavigationService<NavigationStore, MainViewModel>>();
        navigationService.Navigate();

        BindingPlugins.DataValidators.RemoveAt(0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = mainWindow;
            desktop.MainWindow.Show();
        }

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
        services.AddTransient<AnotherPageViewModel>();
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
