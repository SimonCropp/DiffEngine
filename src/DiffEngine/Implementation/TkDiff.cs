﻿using System;
using DiffEngine;

static partial class Implementation
{
    public static Definition TkDiff()
    {
        return new Definition(
            name: DiffTool.TkDiff,
            url: "https://sourceforge.net/projects/tkdiff/",
            autoRefresh: false,
            isMdi: false,
            supportsText: true,
            requiresTarget: true,
            windows: null,
            linux: null,
            osx: new OsSettings(
                (temp, target) => $"\"{temp}\" \"{target}\"",
                "/Applications/TkDiff.app/Contents/MacOS/tkdiff"),
            binaryExtensions: Array.Empty<string>());
    }
}