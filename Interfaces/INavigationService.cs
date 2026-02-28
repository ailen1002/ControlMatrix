// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 02月28日 13:02
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ControlMatrix\Interfaces\INavigationService.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System.Threading.Tasks;
using Avalonia.Controls;
using ControlMatrix.Enums;

namespace ControlMatrix.Interfaces;

public interface INavigationService
{
    void Register<TWindow, TViewModel>(PageKey key)
        where TWindow : Window, new()
        where TViewModel : class;

    void Register<TWindow>(PageKey key)
        where TWindow : Window, new();

    void Navigate(PageKey key);

    Task ShowDialogAsync(PageKey key, Window owner);
}