﻿using System;
using DiffEngine;

static partial class Implementation
{
    public static ToolDefinition TortoiseMerge()
    {
        string BuildArguments(string tempFile, string targetFile) =>
            $"\"{tempFile}\" \"{targetFile}\"";

        return new ToolDefinition(
            name: DiffTool.TortoiseMerge,
            url: "https://tortoisesvn.net/TortoiseMerge.html",
            supportsAutoRefresh: false,
            isMdi: false,
            supportsText: true,
            requiresTarget: true,
            windowsArguments: BuildArguments,
            linuxArguments: BuildArguments,
            osxArguments: BuildArguments,
            windowsExePaths: new[]
            {
                @"%ProgramFiles%\TortoiseSVN\bin\TortoiseMerge.exe"
            },
            binaryExtensions: Array.Empty<string>(),
            linuxExePaths: Array.Empty<string>(),
            osxExePaths: Array.Empty<string>());
    }
}