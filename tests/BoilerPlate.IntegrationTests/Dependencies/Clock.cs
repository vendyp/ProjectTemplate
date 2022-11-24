using BoilerPlate.Shared.Abstraction.Time;

namespace BoilerPlate.IntegrationTests.Dependencies;

public class Clock : IClock
{
    public DateTime CurrentDate() => DateTime.UtcNow;

    public DateTime CurrentServerDate() => CurrentDate();
}