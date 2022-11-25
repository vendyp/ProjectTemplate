using BoilerPlate.Shared.Abstraction.Time;

namespace BoilerPlate.IntegrationTests.Dependencies;

internal class Clock : IClock
{
    public DateTime CurrentDate() => DateTime.UtcNow;

    public DateTime CurrentServerDate() => CurrentDate();
}