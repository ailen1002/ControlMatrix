// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 03月16日 09:03
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ControlMatrix\Modbus\ModbusRtuClientService.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System.Threading.Tasks;
using ControlMatrix.Interfaces;
using System.IO.Ports;
using Modbus.Device;

namespace ControlMatrix.Modbus;

public class ModbusRtuClientService : BaseModbusClient, IModbusRtuClient
{
    private SerialPort? _serialPort;

    private string _portName = "";
    private int _baudRate;
    private int _dataBits;
    private Parity _parity;
    private StopBits _stopBits;
    private int _timeout;
    private int _delay;

    public async Task<bool> ConnectAsync(
        string portName,
        int baudRate,
        int dataBits,
        Parity parity,
        StopBits stopBits,
        int timeout,
        int delay)
    {
        _portName = portName;
        _baudRate = baudRate;
        _dataBits = dataBits;
        _parity = parity;
        _stopBits = stopBits;
        _timeout = timeout;
        _delay = delay;

        return await Task.Run(() =>
        {
            try
            {
                _serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);

                _serialPort.Open();

                _master = ModbusSerialMaster.CreateRtu(_serialPort);

                _master.Transport.ReadTimeout = timeout;
                _master.Transport.WriteTimeout = timeout;
                _master.Transport.Retries = 0;

                IsConnected = true;

                return true;
            }
            catch
            {
                IsConnected = false;
                return false;
            }
        });
    }

    public override async Task<bool> ReconnectAsync()
    {
        Disconnect();

        return await ConnectAsync(
            _portName,
            _baudRate,
            _dataBits,
            _parity,
            _stopBits,
            _timeout,
            _delay);
    }

    public override void Disconnect()
    {
        IsConnected = false;

        _master?.Dispose();

        if (_serialPort?.IsOpen == true)
            _serialPort.Close();
    }
}