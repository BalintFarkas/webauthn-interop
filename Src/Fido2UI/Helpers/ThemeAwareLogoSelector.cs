using System;
using System.Globalization;
using System.Windows.Data;
using ModernWpf;

namespace DSInternals.Win32.WebAuthn.Fido2UI;

/// <summary>
/// Selects the appropriate logo (light or dark) based on the current theme.
/// </summary>
public class ThemeAwareLogoSelector : IMultiValueConverter
{
    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values == null || values.Length < 2)
        {
            return null;
        }

        // Load the logos
        string? lightLogo = values[0] as string;
        string? darkLogo = values[1] as string;

        // Determine if dark theme is active
        bool isDarkTheme = ThemeManager.Current.ActualApplicationTheme == ApplicationTheme.Dark;

        // Select the appropriate logo based on theme, with fallback
        return isDarkTheme
            ? (darkLogo ?? lightLogo)  // Prefer dark, fallback to light
            : (lightLogo ?? darkLogo); // Prefer light, fallback to dark
    }

    public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
