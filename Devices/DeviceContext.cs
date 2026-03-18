// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 03月17日 08:03
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ControlMatrix\Boards\DeviceContext.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

namespace ControlMatrix.Devices;

public class DeviceContext(
    SwitchInputBoard switchInput,
    RelayOutputBoard relayOutput)
{
    public SwitchInputBoard SwitchInput { get; } = switchInput;
    public RelayOutputBoard RelayOutput { get; } = relayOutput;
}