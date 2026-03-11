using System;
using System.Globalization;
using System.Windows.Data;
using Microsoft.Win32;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

/// <summary>
/// Selects the appropriate logo (light or dark) based on the current theme.
/// </summary>
internal sealed class ThemeAwareLogoSelector : IMultiValueConverter
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

        // Determine if dark theme is active by checking Windows settings
        bool isDarkTheme = IsWindowsAppDarkTheme();

        // Select the appropriate logo based on theme, with fallback
        return isDarkTheme
            ? (darkLogo ?? lightLogo)  // Prefer dark, fallback to light
            : (lightLogo ?? darkLogo); // Prefer light, fallback to dark
    }

    private static bool IsWindowsAppDarkTheme()
    {
        // Check Windows 10/11 app theme setting from registry
        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            var value = key?.GetValue("AppsUseLightTheme");
            if (value is int intValue)
            {
                return intValue == 0; // 0 = dark theme, 1 = light theme
            }
        }
        catch
        {
            // Ignore registry access errors
        }

        return false; // Default to light theme
    }

    public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
