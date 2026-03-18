// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 03月17日 10:03
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ControlMatrix\Devices\DeviceManager.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System.Threading.Tasks;

namespace ControlMatrix.Devices;

public class DeviceManager(DeviceContext device)
{
    public async Task StartAsync()
    {
        await device.SwitchInput.InitializeAsync();
        await device.RelayOutput.InitializeAsync();
    }
}