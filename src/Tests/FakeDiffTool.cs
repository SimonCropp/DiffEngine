﻿using System;
using System.IO;
using System.Runtime.InteropServices;

public class FakeDiffTool
{
    public static string Exe;

    static FakeDiffTool()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Exe = Path.Combine(AssemblyLocation.CurrentDirectory, "../../../../FakeDiffTool/bin/win-x64/FakeDiffTool.exe");
            return;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Exe = Path.Combine(AssemblyLocation.CurrentDirectory, "../../../../FakeDiffTool/bin/osx-x64/FakeDiffTool.exe");
            return;
        }

        throw new Exception();
    }
}