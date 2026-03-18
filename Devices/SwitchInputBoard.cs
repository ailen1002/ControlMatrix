// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 03月17日 08:03
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ControlMatrix\Boards\SwitchInputBoard.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System;
using System.Threading.Tasks;
using ControlMatrix.Modbus;

namespace ControlMatrix.Devices
{
    public class SwitchInputBoard
    {
        private readonly ModbusTcpClientService _client = new();

        private const string Ip = "192.168.1.105";
        private const int Port = 502;

        public bool[] Inputs { get; } = new bool[16];

        public async Task InitializeAsync()
        {
            await _client.ConnectAsync(Ip, Port, 3);
        }

        public async Task ReadAsync()
        {
            try
            {
                var regs = await _client.ReadHoldingRegistersAsync(1, 0, 16);

                var value = regs[0];

                for (var i = 0; i < 16; i++)
                {
                    Inputs[i] = (value & (1 << i)) != 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
                await ReconnectAsync();
            }
        }

        private async Task ReconnectAsync()
        {
            await _client.ReconnectAsync();
        }
    }
}