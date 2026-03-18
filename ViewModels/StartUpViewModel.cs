// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 03月05日 08:03
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ControlMatrix\ViewModels\StartUpViewModel.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System;
using System.Threading.Tasks;
using ControlMatrix.Devices;
using ControlMatrix.Interfaces;
using ControlMatrix.Models;

namespace ControlMatrix.ViewModels;

public class StartUpViewModel : ViewModelBase
{
    private readonly INavigationService _nav;
    private readonly DeviceManager _deviceManager;
    public StartUpViewModel(INavigationService nav, DeviceManager deviceManager)
    {
        _nav = nav;
        _deviceManager = deviceManager;
        _ = RunStartup();
    }

    private async Task RunStartup()
    {
        try
        {
            await _deviceManager.StartAsync();
            
            await Task.Delay(10000);
            
            _nav.Navigate(PageKey.MainTest);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"启动失败: {ex.Message}");
        }
    }
}

