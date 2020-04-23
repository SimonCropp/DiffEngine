﻿using System;
using DiffEngine;

static partial class Implementation
{
    public static Definition TortoiseGitMerge()
    {
        return new Definition(
            name: DiffTool.TortoiseGitMerge,
            url: "https://tortoisegit.org/docs/tortoisegitmerge/",
            autoRefresh: false,
            isMdi: false,
            supportsText: true,
            requiresTarget: true,
            windows: new OsSettings(
                (temp, target) => $"\"{temp}\" \"{target}\"",
                @"%ProgramFiles%\TortoiseGit\bin\TortoiseGitMerge.exe"),
            linux: null,
            osx: null,
            binaryExtensions: Array.Empty<string>());
    }
}