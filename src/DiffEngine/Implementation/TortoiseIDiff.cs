﻿using System;
using DiffEngine;

static partial class Implementation
{
    public static ToolDefinition TortoiseIDiff()
    {
        string BuildArguments(string tempFile, string targetFile) =>
            $"/left:\"{tempFile}\" /right:\"{targetFile}\"";

        return new ToolDefinition(
            name: DiffTool.TortoiseIDiff,
            url: "https://tortoisesvn.net/TortoiseIDiff.html",
            supportsAutoRefresh: false,
            isMdi: false,
            supportsText: false,
            requiresTarget: true,
            windowsArguments: BuildArguments,
            linuxArguments: BuildArguments,
            osxArguments: BuildArguments,
            windowsPaths: new[]
            {
                @"%ProgramFiles%\TortoiseSVN\bin\TortoiseIDiff.exe"
            },
            binaryExtensions: new[]
            {
                "bmp",
                "gif",
                "ico",
                "jpg",
                "jpeg",
                "png",
                "tif",
                "tiff",
            },
            linuxPaths: Array.Empty<string>(),
            osxPaths: Array.Empty<string>());
    }
}