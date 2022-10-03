namespace BoilerPlate.Shared.Abstraction.Time;

public interface IClock
{
    DateTime CurrentDate();
    DateTime CurrentServerDate();
}