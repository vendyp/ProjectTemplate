using BoilerPlate.Shared.Abstraction.Time;
using Microsoft.Extensions.DependencyInjection;

namespace BoilerPlate.Shared.Infrastructure.Time;

public static class Extensions
{
    public static void AddClock(this IServiceCollection services)
    {
        var clockOptions = services.GetOptions<ClockOptions>("clock");

        services.AddSingleton(clockOptions)
            .AddSingleton<IClock, Clock>();
    }
}