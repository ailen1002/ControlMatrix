// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 03月05日 13:03
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ControlMatrix\Interfaces\INavigationService.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using Avalonia.Controls;
using ControlMatrix.Models;

namespace ControlMatrix.Interfaces;

public interface INavigationService
{
    void Navigate(PageKey pageKey);
    void ShowDialog(PageKey pageKey, Window owner);
}