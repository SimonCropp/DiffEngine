﻿using System;
using DiffEngine;

static partial class Implementation
{
    public static ToolDefinition KDiff3() =>
        new ToolDefinition(
            name: DiffTool.KDiff3,
            url: "https://github.com/KDE/kdiff3",
            supportsAutoRefresh: false,
            isMdi: false,
            supportsText: true,
            requiresTarget: true,
            buildArguments: (tempFile, targetFile) => $"\"{tempFile}\" \"{targetFile}\"  --cs CreateBakFiles=0",
            windowsExePaths: new[]
            {
                @"%ProgramFiles%\KDiff3\kdiff3.exe"
            },
            binaryExtensions: Array.Empty<string>(),
            linuxExePaths: Array.Empty<string>(),
            osxExePaths: new[]
            {
                "/Applications/kdiff3.app/Contents/MacOS/kdiff3"
            },
            notes: @"
 * `--cs CreateBakFiles=0` to not save a `.orig` file when merging");
}