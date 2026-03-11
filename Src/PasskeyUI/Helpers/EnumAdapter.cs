using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

internal sealed class EnumAdapter : IValueConverter
{
    public static IList<KeyValuePair<T?, string>> GetComboBoxItems<T>(string? nullDisplayName = null) where T : struct, Enum
    {
        var result = new List<KeyValuePair<T?, string>>();

        if (nullDisplayName != null)
        {
            result.Add(new KeyValuePair<T?, string>(null, nullDisplayName));
        }

        var values = Enum.GetValues<T>().Cast<T>().ToList();

        foreach (var value in values)
        {
            result.Add(new KeyValuePair<T?, string>(value, Enum.GetName<T>(value)));
        }

        return result;
    }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value?.ToString();

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
