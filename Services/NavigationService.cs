using System;
using System.Collections.Generic;
using Avalonia.Controls;
using ControlMatrix.Interfaces;
using ControlMatrix.Models;
using ControlMatrix.ViewModels;
using ControlMatrix.Views;
using Microsoft.Extensions.DependencyInjection;

namespace ControlMatrix.Services;

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _services;

    private readonly Dictionary<PageKey, Func<Window>> _routes = new();
    
    private Window? _currentWindow;

    public NavigationService(IServiceProvider services)
    {
        _services = services;

        RegisterRoutes();
    }

    // -------------------------
    // 路由注册
    // -------------------------

    private void RegisterRoutes()
    {
        Register(PageKey.StartUp, () => new StartUpView
        {
            DataContext = _services.GetRequiredService<StartUpViewModel>()
        });

        Register(PageKey.Config, () => new ConfigView
        {
            DataContext = _services.GetRequiredService<ConfigViewModel>()
        });
        
        Register(PageKey.MainTest, () => new MainTestView
        {
            DataContext = _services.GetRequiredService<MainTestViewModel>()
        });
    }

    private void Register(PageKey key, Func<Window> factory)
    {
        _routes[key] = factory;
    }

    // -------------------------
    // 创建 Window
    // -------------------------

    private Window Create(PageKey pageKey)
    {
        if (!_routes.TryGetValue(pageKey, out var factory))
            throw new ArgumentOutOfRangeException(nameof(pageKey), pageKey, "未注册页面");

        return factory();
    }

    // -------------------------
    // 打开窗口
    // -------------------------

    public void Navigate(PageKey pageKey)
    {
        var window = Create(pageKey);
        window.Show();
        _currentWindow?.Close();
        _currentWindow = window;
    }

    // -------------------------
    // 打开 Dialog
    // -------------------------

    public void ShowDialog(PageKey pageKey, Window owner)
    {
        var window = Create(pageKey);
        window.ShowDialog(owner);
    }
}