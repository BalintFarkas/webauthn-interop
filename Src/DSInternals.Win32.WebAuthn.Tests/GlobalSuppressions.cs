using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Naming",
    "CA1707:Identifiers should not contain underscores",
    Justification = "Test methods have underscores in them intentionally for readability.",
    Scope = "module")]

[assembly: SuppressMessage(
    "Design",
    "CA1515:Consider making public types internal",
    Justification = "Test classes are intentionally public for test discovery compatibility across targets.",
    Scope = "module")]
