// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 03月16日 09:03
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ControlMatrix\Modbus\BaseModbusClient.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System;
using System.Threading.Tasks;
using ControlMatrix.Services;
using Modbus.Device;

namespace ControlMatrix.Modbus;

public abstract class BaseModbusClient
{
    protected IModbusMaster? _master;
    private readonly ModbusQueue _queue = new();

    public bool IsConnected { get; protected set; }
    
    public async Task<ushort[]> ReadHoldingRegistersAsync(byte slaveId, ushort start, ushort count)
    {
        return await _queue.EnqueueAsync(() =>
        {
            if (_master == null)
                throw new InvalidOperationException("Modbus not connected");

            return Task.FromResult(
                _master.ReadHoldingRegisters(slaveId, start, count));
        });
    }

    public async Task<ushort[]> ReadInputRegistersAsync(byte slaveId, ushort start, ushort count)
    {
        return await _queue.EnqueueAsync(() =>
        {
            if (_master == null)
                throw new InvalidOperationException("Modbus not connected");

            return Task.FromResult(
                _master.ReadInputRegisters(slaveId, start, count));
        });
    }

    public async Task WriteSingleRegisterAsync(byte slaveId, ushort address, ushort value)
    {
        await _queue.EnqueueAsync(() =>
        {
            if (_master == null)
                throw new InvalidOperationException("Modbus not connected");

            _master.WriteSingleRegister(slaveId, address, value);

            return Task.CompletedTask;
        });
    }

    public async Task WriteRegistersAsync(byte slaveId, ushort start, ushort[] values)
    {
        await _queue.EnqueueAsync(() =>
        {
            if (_master == null)
                throw new InvalidOperationException("Modbus not connected");

            _master.WriteMultipleRegisters(slaveId, start, values);

            return Task.CompletedTask;
        });
    }

    public abstract Task<bool> ReconnectAsync();

    public abstract void Disconnect();
}