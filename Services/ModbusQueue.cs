// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 03月11日 11:03
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ControlMatrix\Services\ModbusQueue.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ControlMatrix.Services;

public class ModbusQueue
{
    private readonly Channel<Func<Task>> _channel = Channel.CreateUnbounded<Func<Task>>();

    public ModbusQueue()
    {
        _ = ProcessLoop();
    }

    public async Task<T> EnqueueAsync<T>(Func<Task<T>> action)
    {
        var tcs = new TaskCompletionSource<T>();

        await _channel.Writer.WriteAsync(async () =>
        {
            try
            {
                var result = await action();
                tcs.SetResult(result);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        });

        return await tcs.Task;
    }

    public async Task EnqueueAsync(Func<Task> action)
    {
        await _channel.Writer.WriteAsync(async () =>
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Modbus Queue Error]: {ex.Message}");
            }
        });
    }

    private async Task ProcessLoop()
    {
        await foreach (var action in _channel.Reader.ReadAllAsync())
        {
            await action();
        }
    }
}