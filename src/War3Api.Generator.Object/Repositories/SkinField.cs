using System.Collections.Generic;

namespace War3Api.Generator.Object.Repositories;

public class SkinField
{
    private readonly Dictionary<int, string> _valuesByLevel = new();

    public void AddForLevel(int level, string value)
    {
        _valuesByLevel.Add(level, value);
    }

    public bool TryGetValueByLevel(int level, out string value)
    {
        return _valuesByLevel.TryGetValue(level, out value);
    }
}