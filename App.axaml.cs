using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ControlMatrix.Enums;
using ControlMatrix.Interfaces;
using ControlMatrix.Services;
using ControlMatrix.ViewModels;
using ControlMatrix.Views;
using Microsoft.Extensions.DependencyInjection;

namespace ControlMatrix;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection();

        services.AddSingleton<INavigationService, NavigationService>();

        //services.AddTransient<StartUpViewModel>();
        //services.AddTransient<ConfigFileViewModel>();
        services.AddTransient<MainWindowViewModel>();

        var provider = services.BuildServiceProvider();

        var nav = provider.GetRequiredService<INavigationService>();;

        //nav.Register<StartUpView, StartUpViewModel>(PageKey.StartUp);
        //nav.Register<ConfigFileView, ConfigFileViewModel>(PageKey.ConfigFile);
        nav.Register<MainWindow, MainWindowViewModel>(PageKey.MainTest);
        //nav.Register<ErrorView>(PageKey.Error);

        nav.Navigate(PageKey.MainTest);
        base.OnFrameworkInitializationCompleted();
    }
}