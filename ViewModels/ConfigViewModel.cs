// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 03月04日 15:03
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ControlMatrix\ViewModels\ConfigViewModel.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System.Reactive;
using ControlMatrix.Interfaces;
using ControlMatrix.Models;
using ReactiveUI;

namespace ControlMatrix.ViewModels;

public class ConfigViewModel : ViewModelBase
{
    private readonly INavigationService _nav;
    public string Greeting { get; } = "程序配置页面";
    public ReactiveCommand<Unit, Unit> StartCommand { get; }
    
    public ConfigViewModel(INavigationService nav)
    {
        _nav = nav;

        StartCommand = ReactiveCommand.Create(Start);
    }

    private void Start()
    {
        _nav.Navigate(PageKey.StartUp);
    }
}