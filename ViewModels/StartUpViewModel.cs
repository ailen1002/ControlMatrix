// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 03月05日 08:03
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ControlMatrix\ViewModels\StartUpViewModel.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System.Threading.Tasks;
using ControlMatrix.Interfaces;
using ControlMatrix.Models;

namespace ControlMatrix.ViewModels;

public class StartUpViewModel : ViewModelBase
{
    private readonly INavigationService _nav;
    
    public StartUpViewModel(INavigationService nav)
    {
        _nav = nav;

        _ = RunStartup();
    }

    private async Task RunStartup()
    {
        await Task.Delay(10000);

        _nav.Navigate(PageKey.MainTest);
    }
}

