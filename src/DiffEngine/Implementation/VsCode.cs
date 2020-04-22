﻿using System;
using DiffEngine;

static partial class Implementation
{
    public static Definition VsCode()
    {
        string BuildArguments(string tempFile, string targetFile) =>
            $"--diff \"{targetFile}\" \"{tempFile}\"";

        return new Definition(
            name: DiffTool.VisualStudioCode,
            url: "https://code.visualstudio.com",
            autoRefresh: true,
            isMdi: true,
            supportsText: true,
            requiresTarget: true,
            windowsArguments: BuildArguments,
            linuxArguments: BuildArguments,
            osxArguments: BuildArguments,
            windowsPaths: new[]
            {
                @"%LocalAppData%\Programs\Microsoft VS Code\code.exe",
                @"%ProgramFiles%\Microsoft VS Code\bin\code"
            },
            binaryExtensions: Array.Empty<string>(),
            linuxPaths: new[]
            {
                @"/usr/local/bin/code"
            },
            osxPaths: new[]
            {
                "/Applications/Visual Studio Code.app/Contents/Resources/app/bin/code"
            },
            notes: @"
 * [Command line reference](https://code.visualstudio.com/docs/editor/command-line)");
    }
}