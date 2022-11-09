using BoilerPlate.Shared.Abstraction.Primitives;

namespace BoilerPlate.Shared.Infrastructure.Api;

/// <summary>
/// Contains the API errors.
/// </summary>
public static class Errors
{
    /// <summary>
    /// Gets the un-processable request error.
    /// </summary>
    public static Error UnProcessableRequest => new(
        "API.UnProcessableRequest",
        "The server could not process the request.");

    /// <summary>
    /// Gets the server error error.
    /// </summary>
    public static Error ServerError => new("API.ServerError", "The server encountered an unrecoverable error.");
}