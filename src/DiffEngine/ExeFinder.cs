﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

static class ExeFinder
{
    public static bool TryFindExe(
        IEnumerable<string> windowsPaths,
        IEnumerable<string> linuxPaths,
        IEnumerable<string> osxPaths,
        [NotNullWhen(true)] out string? path)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return TryFindExe(windowsPaths, out path);
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return TryFindExe(linuxPaths, out path);
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return TryFindExe(osxPaths, out path);
        }

        throw new Exception($"OS not supported: {RuntimeInformation.OSDescription}");
    }

    static bool TryFindExe(IEnumerable<string> paths, [NotNullWhen(true)] out string? exePath)
    {
        foreach (var path in paths)
        {
            if (WildcardFileFinder.TryFind(path, out exePath))
            {
                return true;
            }
        }

        exePath = null;
        return false;
    }
}