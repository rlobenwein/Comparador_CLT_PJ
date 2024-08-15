using System.ComponentModel;
using System;
using System;
using System.Linq;
using System.Reflection;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum value)
    {
        var type = value.GetType();
        var memberInfo = type.GetMember(value.ToString());
        var attribute = memberInfo[0].GetCustomAttribute<DisplayNameAttribute>();

        return attribute == null ? value.ToString() : attribute.DisplayName;
    }
}

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
sealed class DisplayNameAttribute : Attribute
{
    public string DisplayName { get; }

    public DisplayNameAttribute(string displayName) => DisplayName = displayName;
}

namespace Comparativo_CLT_PJ.Enums
{
    public enum Periodicity
    {
        [DisplayName("Hora")]
        Hour,
        [DisplayName("Mês")]
        Month,
        [DisplayName("Ano")]
        Year,
    }
}
