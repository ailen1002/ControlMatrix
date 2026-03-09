using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ControlMatrix.Interfaces;
using ControlMatrix.Models;
using ControlMatrix.Services;
using ControlMatrix.ViewModels;
using ControlMatrix.Views;
using Microsoft.Extensions.DependencyInjection;

namespace ControlMatrix;

public partial class App : Application
{
    private static IServiceProvider Services { get; set; } = null!;
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection();

        // -----------------------------
        // 注册 ViewModel
        // -----------------------------

        services.AddSingleton<MainWindowViewModel>();

        services.AddTransient<ConfigViewModel>();
        services.AddTransient<StartUpViewModel>();
        services.AddTransient<MainTestViewModel>();

        // -----------------------------
        // 注册服务
        // -----------------------------

        services.AddSingleton<INavigationService, NavigationService>();

        // -----------------------------
        // 注册 Window
        // -----------------------------

        services.AddSingleton<MainWindow>();

        Services = services.BuildServiceProvider();

        // -----------------------------
        // 创建主窗口
        // -----------------------------

        var nav = Services.GetRequiredService<INavigationService>();
        nav.Navigate(PageKey.Config);

        base.OnFrameworkInitializationCompleted();
    }
}