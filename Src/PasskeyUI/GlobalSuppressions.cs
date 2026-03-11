using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Design",
    "CA1515:Consider making public types internal",
    Justification = "WPF XAML-generated and UI-facing types are intentionally public.",
    Scope = "module")]

[assembly: SuppressMessage(
    "Performance",
    "CA1812:Avoid uninstantiated internal classes",
    Justification = "WPF and Prism instantiate internal view models and converters via XAML and reflection.",
    Scope = "module")]
