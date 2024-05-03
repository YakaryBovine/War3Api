using System.Collections.Generic;

namespace War3Api.Generator.Object.Repositories;

public class SkinData
{
    private readonly Dictionary<string, SkinField> _valuesByField = new();

    public void AddField(string fieldName, SkinField skinField)
    {
        _valuesByField.Add(fieldName, skinField);
    }

    public bool TryAddField(string fieldName, SkinField skinField)
    {
        return _valuesByField.TryAdd(fieldName, skinField);
    }

    public bool TryGetField(string fieldName, out SkinField skinField)
    {
        return _valuesByField.TryGetValue(fieldName, out skinField);
    }
}