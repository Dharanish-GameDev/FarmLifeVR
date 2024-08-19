using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class ConditionalFieldAttribute : PropertyAttribute
{
    public string ConditionFieldName { get; private set; }
    public object ExpectedValue { get; private set; }

    public ConditionalFieldAttribute(string conditionFieldName, object expectedValue)
    {
        ConditionFieldName = conditionFieldName;
        ExpectedValue = expectedValue;
    }
}
