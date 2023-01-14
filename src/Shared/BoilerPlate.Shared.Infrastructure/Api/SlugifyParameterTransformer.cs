using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;

namespace BoilerPlate.Shared.Infrastructure.Api;

public partial class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        return value is null ? null : MyRegex().Replace(value.ToString()!, "$1-$2").ToLower();
    }

    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex MyRegex();
}