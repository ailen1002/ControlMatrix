// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 03月16日 09:03
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ControlMatrix\Modbus\ModbusTcpClientService.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using ControlMatrix.Interfaces;
using Modbus.Device;

namespace ControlMatrix.Modbus;

public class ModbusTcpClientService : BaseModbusClient, IModbusTcpClient
{
    private TcpClient? _tcpClient;

    private string _ip = "";
    private int _port;
    private double _timeout;

    public async Task<bool> ConnectAsync(string ip, int port, double timeout)
    {
        _ip = ip;
        _port = port;
        _timeout = timeout;

        try
        {
            _tcpClient?.Close();
            _tcpClient = new TcpClient();

            var task = _tcpClient.ConnectAsync(ip, port);
            if (await Task.WhenAny(task, Task.Delay(TimeSpan.FromSeconds(timeout))) != task)
                throw new TimeoutException();

            _master = ModbusIpMaster.CreateIp(_tcpClient);

            IsConnected = true;

            return true;
        }
        catch
        {
            IsConnected = false;
            return false;
        }
    }

    public override async Task<bool> ReconnectAsync()
    {
        Disconnect();
        return await ConnectAsync(_ip, _port, _timeout);
    }

    public override void Disconnect()
    {
        IsConnected = false;
        _master?.Dispose();
        _tcpClient?.Close();
    }
}