// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 03月16日 08:03
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ControlMatrix\Interfaces\IModbusRtuClient.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System.IO.Ports;
using System.Threading.Tasks;

namespace ControlMatrix.Interfaces;

public interface IModbusRtuClient
{
    bool IsConnected { get; }
    Task<bool> ConnectAsync(string portName, int baudRate, int dataBits, Parity parity, StopBits stopBits, int timeout, int delay);
    Task<bool> ReconnectAsync();
    Task<ushort[]> ReadHoldingRegistersAsync(byte slaveId, ushort startAddress, ushort numberOfPoints);
    Task<ushort[]> ReadInputRegistersAsync(byte slaveId, ushort startAddress, ushort numberOfPoints);
    Task WriteSingleRegisterAsync(byte slaveId, ushort address, ushort value);
    Task WriteRegistersAsync(byte slaveId, ushort startAddress, ushort[] values);
    void Disconnect();
}