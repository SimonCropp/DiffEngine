﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace DiffEngine
{
    public static class ProcessCleanup
    {
        static List<ProcessCommand> commands;
        static Func<IEnumerable<ProcessCommand>> findAll;
        static Func<ProcessCommand, bool> tryTerminateProcess;

#pragma warning disable CS8618
        static ProcessCleanup()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                findAll = WindowsProcess.FindAll;
                tryTerminateProcess = WindowsProcess.TryTerminateProcess;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                findAll = LinuxProcess.FindAll;
                tryTerminateProcess = LinuxProcess.TryTerminateProcess;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                findAll = OsxProcess.FindAll;
                tryTerminateProcess = OsxProcess.TryTerminateProcess;
            }
            else
            {
                throw new Exception("Unknown OS");
            }

            Refresh();
        }

        public static IReadOnlyList<ProcessCommand> Commands => commands;

        public static void Refresh()
        {
            commands = findAll().ToList();
        }

        /// <summary>
        /// Find a process with the matching command line and kill it.
        /// </summary>
        public static void Kill(string command)
        {
            Guard.AgainstNullOrEmpty(command, nameof(command));
            var trimmedCommand = command.Replace("\"", "");
            var matchingCommands = Commands
                .Where(x => x.Command.Replace("\"", "") == trimmedCommand).ToList();
            Logging.Write($"Kill: {command}. Matching count: {matchingCommands.Count}");
            if (matchingCommands.Count == 0)
            {
                var separator = Environment.NewLine + "\t";
                Logging.Write($"No matching commands. All commands: {separator}{string.Join(separator, Commands.Select(x => x.Command))}.");
            }

            foreach (var processCommand in matchingCommands)
            {
                TerminateProcessIfExists(processCommand);
            }
        }

        public static bool IsRunning(string command)
        {
            Guard.AgainstNullOrEmpty(command, nameof(command));
            return commands.Any(x => x.Command == command);
        }

        static void TerminateProcessIfExists(in ProcessCommand processCommand)
        {
            var processId = processCommand.Process;
            if (tryTerminateProcess(processCommand))
            {
                Logging.Write($"TerminateProcess. Id: {processId}.");
            }
            else
            {
                Logging.Write($"Process not valid. Id: {processId}.");
            }
        }

        /// <summary>
        /// Find all processes with `% %.%.%` in the command line.
        /// </summary>
        public static IEnumerable<ProcessCommand> FindAll()
        {
            return findAll();
        }
    }
}