﻿using System;
using DiffEngine;

static partial class Implementation
{
    public static Definition KDiff3()
    {
        string Arguments(string temp, string target) =>
            $"\"{temp}\" \"{target}\" --cs CreateBakFiles=0";

        return new Definition(
            name: DiffTool.KDiff3,
            url: "https://github.com/KDE/kdiff3",
            autoRefresh: false,
            isMdi: false,
            supportsText: true,
            requiresTarget: true,
            windows:new OsSettings(Arguments, @"%ProgramFiles%\KDiff3\kdiff3.exe"),
            linux: null,
            osx: new OsSettings(Arguments, "/Applications/kdiff3.app/Contents/MacOS/kdiff3"),
            binaryExtensions: Array.Empty<string>(),
            notes: @"
 * `--cs CreateBakFiles=0` to not save a `.orig` file when merging");
    }
}