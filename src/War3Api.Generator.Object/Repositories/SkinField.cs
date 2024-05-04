using System.Collections.Generic;

namespace War3Api.Generator.Object.Repositories;

public sealed class SkinField
{
    private readonly Dictionary<int, string> _valuesByLevel;

    public string Value { get; }

    public SkinField(string value)
    {
        _valuesByLevel = SplitValueByLevel(value);
        Value = value;
    }

    public bool TryGetValueByLevel(int level, out string value)
    {
        return _valuesByLevel.TryGetValue(level, out value);
    }

    private static Dictionary<int, string> SplitValueByLevel(string value)
    {
        var i = 1;
        var valuesByLevel = new Dictionary<int, string>();
        var splitType = value.StartsWith("\"") ? "\",\"" : ",";

        foreach (var leveledValue in value.Split(splitType))
        {
            valuesByLevel.Add(i, leveledValue);
            i++;
        }

        return valuesByLevel;
    }
}