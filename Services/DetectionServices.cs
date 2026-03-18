// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 03月11日 13:03
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ControlMatrix\Services\DetectionServices.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System;
using System.Threading.Tasks;
using ControlMatrix.Devices;
using ControlMatrix.Models;

namespace ControlMatrix.Services;

public class DetectionServices(DeviceContext devices)
{
    public async Task<bool> TestAsync()
    {
        while (true)
        {
            await devices.RelayOutput.SetAsync(RelayOutput.ApSwitch, true);

            var status = await devices.RelayOutput.GetAsync(RelayOutput.ApSwitch);
            
            Console.WriteLine($"ApSwitch状态 on：{status}" );

            await Task.Delay(1000);
        
            await devices.RelayOutput.SetAsync(RelayOutput.ApSwitch, false);
            
            var status1 = await devices.RelayOutput.GetAsync(RelayOutput.ApSwitch);
            
            Console.WriteLine($"ApSwitch状态 off：{status1}" );

            await devices.SwitchInput.ReadAsync();

            Console.WriteLine($"TestAsync: {devices.SwitchInput.Inputs[12]}"); 
        }
    }
}