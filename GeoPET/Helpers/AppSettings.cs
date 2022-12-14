using System.Diagnostics.CodeAnalysis;

namespace GeoPet.Helpers;

[ExcludeFromCodeCoverage]
public class AppSettings
{
    public string Secret { get; set; } = default!;
}