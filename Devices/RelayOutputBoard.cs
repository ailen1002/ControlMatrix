// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 03月10日 10:03
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ControlMatrix\Boards\RelayOutputBoard.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System;
using System.Threading.Tasks;
using ControlMatrix.Modbus;
using ControlMatrix.Models;

namespace ControlMatrix.Devices;

public class RelayOutputBoard
{
    private readonly ModbusTcpClientService _client = new();
    
    private const string Ip = "192.168.1.106";
    private const int Port = 502;
    public bool[] States { get; } = new bool[16];
    
    public async Task InitializeAsync()
    {
        await _client.ConnectAsync(Ip, Port, 3);
    }

    public async Task SetAsync(RelayOutput relay, bool on)
    {
        try
        {
            var value = on ? (ushort)1 : (ushort)0;

            await _client.WriteSingleRegisterAsync(
                1,
                (ushort)relay,
                value);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
            await ReconnectAsync();
        }
    }
    
    public async Task<bool> GetAsync(RelayOutput relay)
    {
        var regs = new ushort[1];
        
        try
        {
            regs = await _client.ReadHoldingRegistersAsync(1, (ushort)relay, 1);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
            await ReconnectAsync();
        }
        
        return regs[0] == 1;
    }

    public async Task RefreshAsync()
    {
        try
        {
            var regs = await _client.ReadHoldingRegistersAsync(1, 0, 16);

            for (var i = 0; i < 16; i++)    
            {
                States[i] = regs[i] == 1;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
            await ReconnectAsync();
        }
    }
    
    public async Task ReconnectAsync()
    {
        await _client.ReconnectAsync();
    }
}