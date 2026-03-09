// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 03月09日 13:03
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ControlMatrix\Services\ModbusTCPClient.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using ControlMatrix.Interfaces;
using Modbus.Device;

namespace ControlMatrix.Services;

public class ModbusTcpClient : IModbusTcpClient
{
    private TcpClient? _tcpClient;
    private ModbusIpMaster? _master;
    private readonly SemaphoreSlim _lock = new(1, 1);

    private string _ip = string.Empty;
    private int _port;

    public bool IsConnected => _tcpClient?.Connected == true;

    public async Task<bool> ConnectAsync(string ip, int port)
    {
        if (IsConnected && _ip == ip && _port == port)
            return true;

        _ip = ip;
        _port = port;

        return await ReconnectAsync();
    }

    public async Task<bool> ReconnectAsync()
    {
        await _lock.WaitAsync();
        try
        {
            Disconnect();

            _tcpClient = new TcpClient();

            var connectTask = _tcpClient.ConnectAsync(_ip, _port);

            if (await Task.WhenAny(connectTask, Task.Delay(3000)) != connectTask)
            {
                Console.WriteLine("[Modbus] 连接超时");
                return false;
            }

            await connectTask;

            _master = ModbusIpMaster.CreateIp(_tcpClient);

            Console.WriteLine($"[Modbus] 已连接 {_ip}:{_port}");

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Modbus] 连接失败: {ex.Message}");
            return false;
        }
        finally
        {
            _lock.Release();
        }
    }

    public void Disconnect()
    {
        try
        {
            _master?.Dispose();
            _tcpClient?.Close();
            _tcpClient?.Dispose();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Modbus] 断开异常: {ex.Message}");
        }
        finally
        {
            _master = null;
            _tcpClient = null;
        }
    }

    private async Task<T?> ExecuteAsync<T>(Func<ModbusIpMaster, Task<T>> action)
    {
        if (!IsConnected)
        {
            if (!await ReconnectAsync())
                return default;
        }

        await _lock.WaitAsync();

        try
        {
            if (_master == null)
                return default;

            return await action(_master);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Modbus] 执行失败: {ex.Message}");

            if (ex is IOException or SocketException)
            {
                Disconnect();
            }

            return default;
        }
        finally
        {
            _lock.Release();
        }
    }

    private async Task ExecuteAsync(Func<ModbusIpMaster, Task> action)
    {
        if (!IsConnected)
        {
            if (!await ReconnectAsync())
                return;
        }

        await _lock.WaitAsync();

        try
        {
            if (_master == null)
                return;

            await action(_master);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Modbus] 执行失败: {ex.Message}");

            if (ex is IOException or SocketException)
            {
                Disconnect();
            }
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task<ushort[]> ReadHoldingRegistersAsync(byte slaveId, ushort startAddress, ushort numberOfPoints)
    {
        return await ExecuteAsync(m =>
            m.ReadHoldingRegistersAsync(slaveId, startAddress, numberOfPoints))
            ?? [];
    }

    public async Task<ushort[]> ReadInputRegistersAsync(byte slaveId, ushort startAddress, ushort numberOfPoints)
    {
        return await ExecuteAsync(m =>
            m.ReadInputRegistersAsync(slaveId, startAddress, numberOfPoints))
            ?? [];
    }

    public async Task WriteSingleRegisterAsync(byte slaveId, ushort address, ushort value)
    {
        await ExecuteAsync(m =>
            m.WriteSingleRegisterAsync(slaveId, address, value));
    }

    public async Task WriteRegistersAsync(byte slaveId, ushort startAddress, ushort[] values)
    {
        await ExecuteAsync(m =>
            m.WriteMultipleRegistersAsync(slaveId, startAddress, values));
    }
}