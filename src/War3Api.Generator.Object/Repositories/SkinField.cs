using System.Collections.Generic;

namespace War3Api.Generator.Object.Repositories;

public sealed class SkinField
{
    private readonly Dictionary<int, string> _valuesByLevel = new();

    public string Value { get; }

    public SkinField(string value)
    {
        Value = value;
        var i = 1;
        foreach (var leveledValue in value.Split(','))
        {
            _valuesByLevel.Add(i, leveledValue);
            i++;
        };
    }

    public bool TryGetValueByLevel(int level, out string value)
    {
        return _valuesByLevel.TryGetValue(level, out value);
    }
}