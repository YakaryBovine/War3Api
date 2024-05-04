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
    private readonly Dictionary<string, SkinData> _skinValuesBySkinId = new();

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
    public bool TryGetValue(string skinId, string fieldName, int level, [NotNullWhen(true)] out object? value)
    {
        if (_skinValuesBySkinId.TryGetValue(skinId, out var skinData))
        {
            if (skinData.TryGetField(fieldName, out var skinField))
            {
                return skinField.TryGetValueByLevel(level, out value);
            }
        }

        value = null;
        return false;
    }

    /// <summary>
    /// Tries to get the value of a particular skin's field, according to any skin file.
    /// </summary>
    /// <returns>True if a value was provided in the out parameter.</returns>
    public bool TryGetValue(string skinId, string fieldName, [NotNullWhen(true)] out object? value)
    {
        if (_skinValuesBySkinId.TryGetValue(skinId, out var skinData))
        {
            if (skinData.TryGetField(fieldName, out var skinField))
            {
                value = skinField.Value;
                return true;
            }
        }

        value = null;
        return false;
    }

    private void AddSkinFileToLookup(string inputFolder, string skinFilePath)
    {
        using var stringsFile = File.OpenRead(Path.Combine(inputFolder, skinFilePath));
        using var stringsReader = new StreamReader(stringsFile);
        SkinData activeSkinData = null;
        while (!stringsReader.EndOfStream)
        {
            var line = stringsReader.ReadLine();

            if (!line.Contains("=") && !line.StartsWith('['))
            {
                continue;
            }

            if (line.StartsWith('['))
            {
                var skinId = line
                    .Replace("[", string.Empty)
                    .Replace("]", string.Empty);

                if (!_skinValuesBySkinId.TryGetValue(skinId, out activeSkinData))
                {
                    activeSkinData = new SkinData();
                    _skinValuesBySkinId.Add(skinId, activeSkinData);
                }
            }
            else
            {
                AddSkinFieldFromLine(activeSkinData, line);
            }
        }
    }

    private static void AddSkinFieldFromLine(SkinData? activeSkinData, string line)
    {
        var splitPosition = line.IndexOf('=', StringComparison.Ordinal);
        var key = line[..splitPosition];
        var values = line[(splitPosition + 1)..];
        //This is a ludicrous workaround for the fact that Button Position, despite being two values, is expressed in
        //one line, e.g. Buttonpos=2,0
        if (key == "Buttonpos")
        {
            var splitValues = values.Split(',');
            AddSkinField(activeSkinData, "Buttonposx", splitValues[0]);
            AddSkinField(activeSkinData, "Buttonposy", splitValues[1]);
        }
        else
        {
            AddSkinField(activeSkinData, key, values);
        }
    }

    private static void AddSkinField(SkinData? activeSkinData, string key, string values)
    {
        var cleanedValues = CleanValue(key, values);
        var newSkinField = new SkinField(cleanedValues);
        activeSkinData!.TryAddField(key, newSkinField);
    }

    private static string CleanValue(string key, string value)
    {
        return key switch
        {
            "Researchtip" => value.Split(",")[0],
            "Casterupgradetip" => value.Replace("\"", string.Empty),
            "Researchubertip" => value.Split('"')[0],
            _ => value
        };
    }
}