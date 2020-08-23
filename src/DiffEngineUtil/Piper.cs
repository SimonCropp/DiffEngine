﻿using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

public static class Piper
{
    public static async Task Send(string[] args, CancellationToken cancellation = default)
    {
        await using var pipe = new NamedPipeClientStream(
            ".",
            "DiffEngineUtil",
            PipeDirection.Out,
            PipeOptions.Asynchronous);
        await using var stream = new StreamWriter(pipe);
        await pipe.ConnectAsync(1000, cancellation);
        var message = string.Join(Environment.NewLine, args);
        await stream.WriteAsync(message.AsMemory(), cancellation);
    }

    public static async Task Start(Action<string[]> receive, CancellationToken cancellation)
    {
        while (true)
        {
            if (cancellation.IsCancellationRequested)
            {
                break;
            }

            await using var pipe = new NamedPipeServerStream(
                "DiffEngineUtil",
                PipeDirection.In,
                1,
                PipeTransmissionMode.Byte,
                PipeOptions.Asynchronous | PipeOptions.CurrentUserOnly);
            await pipe.WaitForConnectionAsync(cancellation);
            using var streamReader = new StreamReader(pipe);
            var message = await streamReader.ReadToEndAsync();
            receive(message.Split(Environment.NewLine));

            if (pipe.IsConnected)
            {
                // must disconnect
                await pipe.DisposeAsync();
            }
        }
    }
}