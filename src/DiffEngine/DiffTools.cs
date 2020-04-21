﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using EmptyFiles;

namespace DiffEngine
{
    public static class DiffTools
    {
        static ConcurrentDictionary<string, ResolvedDiffTool> ExtensionLookup = new ConcurrentDictionary<string, ResolvedDiffTool>();
        static ConcurrentBag<ResolvedDiffTool> resolved = new ConcurrentBag<ResolvedDiffTool>();

        public static IEnumerable<ResolvedDiffTool> Resolved { get => resolved; }

        public static string GetPathFor(DiffTool tool)
        {
            if (TryGetPathFor(tool, out var exePath))
            {
                return exePath;
            }
            throw new Exception($"Tool to found: {tool}");
        }

        public static bool TryGetPathFor(DiffTool tool, [NotNullWhen(true)] out string? exePath)
        {
            var resolvedDiffTool = resolved.SingleOrDefault(x => x.Tool == tool);
            if (resolvedDiffTool == null)
            {
                exePath = null;
                return false;
            }

            exePath = resolvedDiffTool.ExePath;
            return true;
        }

        public static bool AddCustomTool(
            DiffTool basedOn,
            string name,
            bool? supportsAutoRefresh,
            bool? isMdi,
            bool? supportsText,
            bool? requiresTarget,
            BuildArguments? buildArguments,
            string? exePath,
            IEnumerable<string>? binaryExtensions)
        {
            var existing = resolved.SingleOrDefault(x=>x.Tool ==basedOn);
            if (existing == null)
            {
                return false;
            }

            return TryAddCustomTool(
                name,
                supportsAutoRefresh ?? existing.SupportsAutoRefresh,
                isMdi ?? existing.IsMdi,
                supportsText ?? existing.SupportsText,
                requiresTarget ?? existing.RequiresTarget,
                buildArguments ?? existing.BuildArguments,
                exePath ?? existing.ExePath,
                binaryExtensions ?? existing.BinaryExtensions
            );
        }

        public static bool TryAddCustomTool(
            string name,
            bool supportsAutoRefresh,
            bool isMdi,
            bool supportsText,
            bool requiresTarget,
            BuildArguments buildArguments,
            string exePath,
            IEnumerable<string> binaryExtensions)
        {
            Guard.AgainstNullOrEmpty(exePath, nameof(exePath));
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNull(binaryExtensions, nameof(binaryExtensions));
            Guard.AgainstNull(buildArguments, nameof(buildArguments));
            if (!File.Exists(exePath))
            {
                return false;
            }

            if (resolved.Any(x => x.Name == name))
            {
                throw new ArgumentException($"Tool with name already exists. Name: {name}", nameof(name));
            }

            var extensions = binaryExtensions.ToArray();
            var tool = new ResolvedDiffTool(
                name,
                null,
                exePath,
                buildArguments,
                isMdi,
                supportsAutoRefresh,
                extensions,
                requiresTarget,
                supportsText);

            resolved.Add(tool);
            foreach (var extension in extensions)
            {
                var cleanedExtension = Extensions.GetExtension(extension);
                ExtensionLookup[cleanedExtension] = tool;
            }

            return true;
        }

        internal static List<ToolDefinition> Tools()
        {
            return new List<ToolDefinition>
            {
                Implementation.BeyondCompare(),
                Implementation.P4Merge(),
                Implementation.AraxisMerge(),
                Implementation.Meld(),
                Implementation.SublimeMerge(),
                Implementation.Kaleidoscope(),
                Implementation.CodeCompare(),
                Implementation.WinMerge(),
                Implementation.DiffMerge(),
                Implementation.TortoiseMerge(),
                Implementation.TortoiseGitMerge(),
                Implementation.TortoiseIDiff(),
                Implementation.KDiff3(),
                Implementation.TkDiff(),
                Implementation.VsCode(),
                Implementation.VisualStudio(),
                Implementation.Rider()
            };
        }

        static DiffTools()
        {
            var diffOrder = Environment.GetEnvironmentVariable("DiffEngine.ToolOrder");
            if (diffOrder == null)
            {
                diffOrder = Environment.GetEnvironmentVariable("Verify.DiffToolOrder");
            }

            IEnumerable<DiffTool> order;
            bool throwForNoTool;
            if (string.IsNullOrWhiteSpace(diffOrder))
            {
                throwForNoTool = false;
                order = Enum.GetValues(typeof(DiffTool)).Cast<DiffTool>();
            }
            else
            {
                throwForNoTool = true;
                order = ParseEnvironmentVariable(diffOrder);
            }

            var tools = ToolsByOrder(throwForNoTool, order);

            InitLookups(tools);
        }

        public static void UseOrder(params DiffTool[] order)
        {
            UseOrder(false, order);
        }

        public static void UseOrder(bool throwForNoTool, params DiffTool[] order)
        {
            Guard.AgainstNullOrEmpty(order, nameof(order));
            var tools = ToolsByOrder(throwForNoTool, order);

            ExtensionLookup.Clear();
#if NETSTANDARD2_1

            resolved.Clear();
#else
            ResolvedDiffTool someItem;
            while (!resolved.IsEmpty)
            {
                resolved.TryTake(out someItem);
            }
#endif
            InitLookups(tools);
        }

        static void InitLookups(IEnumerable<ToolDefinition> tools)
        {
            foreach (var tool in tools.Reverse())
            {
                var diffTool = new ResolvedDiffTool(
                    tool.Tool.ToString(),
                    tool.Tool,
                    tool.ExePath!,
                    tool.BuildArguments,
                    tool.IsMdi,
                    tool.SupportsAutoRefresh,
                    tool.BinaryExtensions,
                    tool.RequiresTarget,
                    tool.SupportsText);

                resolved.Add(diffTool);
                foreach (var ext in tool.BinaryExtensions)
                {
                    if (!ExtensionLookup.ContainsKey(ext))
                    {
                        ExtensionLookup[ext] = diffTool;
                    }
                }
            }
        }

        internal static IEnumerable<DiffTool> ParseEnvironmentVariable(string diffOrder)
        {
            foreach (var toolString in diffOrder
                .Split(new[] {',', '|', ' '}, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!Enum.TryParse<DiffTool>(toolString, out var diffTool))
                {
                    throw new Exception($"Unable to parse tool from `DiffEngine.DiffToolOrder` environment variable: {toolString}");
                }

                yield return diffTool;
            }
        }

        static IEnumerable<ToolDefinition> ToolsByOrder(bool throwForNoTool, IEnumerable<DiffTool> order)
        {
            var allTools = Tools()
                .Where(x => x.Exists)
                .ToList();
            foreach (var diffTool in order)
            {
                var definition = allTools.SingleOrDefault(x => x.Tool == diffTool);
                if (definition == null)
                {
                    if (!throwForNoTool)
                    {
                        continue;
                    }

                    throw new Exception($"`DiffEngine.DiffToolOrder` is configured to use '{diffTool}' but it is not installed.");
                }

                yield return definition;
                allTools.Remove(definition);
            }

            foreach (var definition in allTools)
            {
                yield return definition;
            }
        }

        internal static bool TryFind(
            string extension,
            [NotNullWhen(true)] out ResolvedDiffTool? tool)
        {
            if (Extensions.IsText(extension))
            {
                return FirstTextTool(out tool);
            }

            return ExtensionLookup.TryGetValue(extension, out tool);
        }

        static bool FirstTextTool(out ResolvedDiffTool tool)
        {
            tool = TextTools().FirstOrDefault();
            return tool != null;
        }

        static IEnumerable<ResolvedDiffTool> TextTools()
        {
            return resolved.Where(x => x.SupportsText);
        }

        internal static bool TryFind(
            DiffTool tool,
            string extension,
            [NotNullWhen(true)] out ResolvedDiffTool? resolvedTool)
        {
            if (Extensions.IsText(extension))
            {
                resolvedTool = TextTools().SingleOrDefault(x => x.Tool == tool);
                return resolvedTool != null;
            }

            resolvedTool = resolved.SingleOrDefault(x => x.Tool == tool);
            if (resolvedTool == null)
            {
                return false;
            }

            if (!resolvedTool.BinaryExtensions.Contains(extension))
            {
                resolvedTool = null;
                return false;
            }

            return true;
        }

        public static bool IsDetectedFor(DiffTool diffTool, string extensionOrPath)
        {
            var extension = Extensions.GetExtension(extensionOrPath);
            if (Extensions.IsText(extension))
            {
                return TextTools().Any();
            }

            var tool = resolved.SingleOrDefault(_ => _.Tool == diffTool);
            if (tool == null)
            {
                return false;
            }

            return tool.BinaryExtensions.Contains(extension);
        }
    }
}