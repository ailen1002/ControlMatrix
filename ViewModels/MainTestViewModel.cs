// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 03月05日 08:03
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ControlMatrix\ViewModels\MainTestViewModel.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System;
using System.Reactive;
using System.Threading.Tasks;
using ControlMatrix.Services;
using ReactiveUI;

namespace ControlMatrix.ViewModels;

public class MainTestViewModel : ViewModelBase
{
    private readonly DetectionServices _detection;

    public string Model { get; } = "测试页面";

    public ReactiveCommand<Unit, Task> TestCommand { get; }

    public MainTestViewModel(DetectionServices detection)
    {
        _detection = detection;

        TestCommand = ReactiveCommand.Create(RunTest);
    }

    private async Task RunTest()
    {
        try
        {
            var result = await _detection.TestAsync();

            Console.WriteLine($"测试结果: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"测试失败: {ex.Message}");
        }
    }
}