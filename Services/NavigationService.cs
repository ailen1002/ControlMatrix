// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 02月28日 13:02
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ControlMatrix\Services\NavigationService.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using ControlMatrix.Enums;
using ControlMatrix.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ControlMatrix.Services;

public class NavigationService(IServiceProvider provider) : INavigationService
{
    private readonly Dictionary<PageKey, Func<Window>> _routes = new();
    private readonly Dictionary<PageKey, Window> _opened = new();
    
    // 注册带 ViewModel 的窗口
    public void Register<TWindow, TViewModel>(PageKey key)
        where TWindow : Window, new()
        where TViewModel : class
    {
        _routes[key] = () =>
        {
            var window = new TWindow
            {
                DataContext = provider.GetRequiredService<TViewModel>()
            };
            return window;
        };
    }

    // 注册无 ViewModel 窗口
    public void Register<TWindow>(PageKey key)
        where TWindow : Window, new()
    {
        _routes[key] = () => new TWindow();
    }

    public void Navigate(PageKey key)
    {
        if (_opened.TryGetValue(key, out var existing))
        {
            existing.Activate();
            return;
        }

        var window = Create(key);
        window.Closed += (_, _) => _opened.Remove(key);

        _opened[key] = window;
        window.Show();
    }

    public async Task ShowDialogAsync(PageKey key, Window owner)
    {
        var window = Create(key);
        await window.ShowDialog(owner);
    }

    private Window Create(PageKey key)
    {
        if (_routes.TryGetValue(key, out var factory))
            return factory();

        throw new ArgumentOutOfRangeException(nameof(key), key, null);
    }
}