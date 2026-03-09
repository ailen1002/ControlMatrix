// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 03月09日 15:03
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ControlMatrix\Interfaces\IModbusTcpClient.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System.Threading.Tasks;

namespace ControlMatrix.Interfaces;

public interface IModbusTcpClient
{
    bool IsConnected { get; }
    Task<bool> ConnectAsync(string ip, int port);
    Task<bool> ReconnectAsync();
    Task<ushort[]> ReadHoldingRegistersAsync(byte slaveId, ushort startAddress, ushort numberOfPoints);
    Task<ushort[]> ReadInputRegistersAsync(byte slaveId, ushort startAddress, ushort numberOfPoints);
    Task WriteSingleRegisterAsync(byte slaveId, ushort address, ushort value);
    Task WriteRegistersAsync(byte slaveId, ushort startAddress, ushort[] values);
    void Disconnect();
}