namespace BoilerPlate.App;

public static class Constant
{
    public static string? GetApiHost(string env)
    {
        return env switch
        {
            "Development" => " http://localhost:5000",
            "Production" => null,
            _ => null
        };
    }
}