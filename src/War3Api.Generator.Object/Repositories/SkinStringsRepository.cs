#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace War3Api.Generator.Object.Repositories;

/// <summary>
/// Contains skin information about all objects in the game.
/// </summary>
public sealed class SkinStringsRepository
{
    private readonly Dictionary<string, Dictionary<string, Dictionary<int, string>>> _skinValuesBySkinId = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="SkinStringsRepository"/> class.
    /// </summary>
    public SkinStringsRepository(string inputFolder, IEnumerable<string> skinFilePaths)
    {
        foreach (var skinFilePath in skinFilePaths)
        {
            AddSkinFileToLookup(inputFolder, skinFilePath);
        }
    }

    /// <summary>
    /// Tries to get the value of a particular skin's field, according to any skin file.
    /// </summary>
    /// <returns>True if a value was provided in the out parameter.</returns>
    public bool TryGetValue(string skinId, string fieldName, int level, [NotNullWhen(true)] out string? value)
    {
        if (_skinValuesBySkinId.TryGetValue(skinId, out var skinValues))
        {
            if (skinValues.TryGetValue(fieldName, out var leveledValues))
            {
                return leveledValues.TryGetValue(level, out value);
            };
        }

        value = null;
        return false;
    }

    private void AddSkinFileToLookup(string inputFolder, string skinFilePath)
    {
        using var stringsFile = File.OpenRead(Path.Combine(inputFolder, skinFilePath));
        using var stringsReader = new StreamReader(stringsFile);
        Dictionary<string, Dictionary<int, string>>? activeSkinValues = null;
        while (!stringsReader.EndOfStream)
        {
            var line = stringsReader.ReadLine();

            if (string.IsNullOrEmpty(line) || line.StartsWith('/'))
            {
                continue;
            }

            if (line.StartsWith('['))
            {
                var skinId = line
                    .Replace("[", string.Empty)
                    .Replace("]", string.Empty);

                if (!_skinValuesBySkinId.TryGetValue(skinId, out activeSkinValues))
                {
                    activeSkinValues = new Dictionary<string, Dictionary<int, string>>();
                    _skinValuesBySkinId.Add(skinId, activeSkinValues);
                }
            }
            else
            {
                var splitPosition = line.IndexOf('=', StringComparison.Ordinal);
                var key = line[..splitPosition];
                var values = line[(splitPosition + 1)..];
                var newSkinValues = new Dictionary<int, string>();
                activeSkinValues!.TryAdd(key, newSkinValues);

                var valuesSplitByLevel = values.Split(',');
                var i = 1;
                foreach (var value in valuesSplitByLevel)
                {
                    var cleanedValue = CleanCasterUpgradeString(key, value);
                    newSkinValues.TryAdd(i, cleanedValue);
                    i++;
                }

            }
        }
    }

    private static string CleanCasterUpgradeString(string key, string value) =>
        key == "Casterupgradetip" ? value.Replace("\"", string.Empty) : value;
}