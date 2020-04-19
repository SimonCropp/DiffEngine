﻿using System;
using System.Diagnostics;
using System.IO;
using EmptyFiles;

namespace DiffEngine
{
    /// <summary>
    /// Manages diff tools processes.
    /// </summary>
    public static class DiffRunner
    {
        static int maxInstancesToLaunch = 5;
        static int launchedInstances;

        public static void MaxInstancesToLaunch(int value)
        {
            Guard.AgainstNegativeAndZero(value, nameof(value));
            maxInstancesToLaunch = value;
        }

        /// <summary>
        /// Find and kill a diff tool process.
        /// </summary>
        public static void Kill(string tempFile, string targetFile)
        {
            var extension = Extensions.GetExtension(tempFile);
            if (!DiffTools.TryFind(extension, out var diffTool))
            {
                return;
            }

            var command = diffTool.BuildCommand(tempFile, targetFile);

            if (diffTool.IsMdi)
            {
                return;
            }

            ProcessCleanup.Kill(command);
        }

        public static LaunchResult Launch(DiffTool tool, string tempFile, string targetFile)
        {
            GuardFiles(tempFile, targetFile);
            var extension = Extensions.GetExtension(tempFile);
            if (!DiffTools.TryFind(tool, extension, out var resolvedTool))
            {
                return LaunchResult.NoDiffToolForExtension;
            }

            return Launch(resolvedTool, tempFile, targetFile);
        }

        /// <summary>
        /// Launch a diff tool for the given paths.
        /// </summary>
        public static LaunchResult Launch(string tempFile, string targetFile)
        {
            GuardFiles(tempFile, targetFile);
            var extension = Extensions.GetExtension(tempFile);

            if (!DiffTools.TryFind(extension, out var diffTool))
            {
                return LaunchResult.NoDiffToolForExtension;
            }

            return Launch(diffTool, tempFile, targetFile);
        }

        static LaunchResult Launch(ResolvedDiffTool diffTool, string tempFile, string targetFile)
        {
            if (CheckInstanceCount())
            {
                return LaunchResult.TooManyRunningDiffTools;
            }

            var targetExists = File.Exists(targetFile);
            if (diffTool.RequiresTarget && !targetExists)
            {
                if (!AllFiles.TryCreateFile(targetFile, true))
                {
                    return LaunchResult.NoEmptyFileForExtension;
                }
            }

            return InnerLaunch(diffTool, tempFile, targetFile);
        }

        static LaunchResult InnerLaunch(ResolvedDiffTool diffTool, string tempFile, string targetFile)
        {
            launchedInstances++;

            var command = diffTool.BuildCommand(tempFile, targetFile);
            var isDiffToolRunning = ProcessCleanup.IsRunning(command);
            if (isDiffToolRunning)
            {
                if (diffTool.SupportsAutoRefresh)
                {
                    return LaunchResult.AlreadyRunningAndSupportsRefresh;
                }

                if (!diffTool.IsMdi)
                {
                    ProcessCleanup.Kill(command);
                }
            }

            var arguments = diffTool.BuildArguments(tempFile, targetFile);
            try
            {
                Process.Start(diffTool.ExePath, arguments);
                return LaunchResult.StartedNewInstance;
            }
            catch (Exception exception)
            {
                var message = $@"Failed to launch diff tool.
{diffTool.ExePath} {arguments}";
                throw new Exception(message, exception);
            }
        }

        static void GuardFiles(string tempFile, string targetFile)
        {
            Guard.FileExists(tempFile, nameof(tempFile));
            Guard.AgainstNullOrEmpty(targetFile, nameof(targetFile));
        }

        static bool CheckInstanceCount()
        {
            return launchedInstances >= maxInstancesToLaunch;
        }
    }
}