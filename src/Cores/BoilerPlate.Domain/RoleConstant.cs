namespace BoilerPlate.Domain;

public static class RoleConstant
{
    public static Dictionary<string, string> Dictionary = new()
    {
        { Administrator, "Administrator" },
        { User, "User" }
    };

    public const string Administrator = "ADMINISTRATOR";
    public const string User = "USER";
}