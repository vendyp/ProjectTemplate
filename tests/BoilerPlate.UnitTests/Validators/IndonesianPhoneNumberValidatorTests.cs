namespace BoilerPlate.UnitTests.Validators;

public class IndonesianPhoneNumberValidatorTests
{
    /// <summary>
    /// Doing testing IndonesianPhoneNumberValidator with data test of GetDataTestPhoneNumberValidator
    /// </summary>
    [Theory]
    [MemberData(nameof(GetDataTestPhoneNumberValidator))]
    public void TestIndonesianValidatorScenario(string s, bool result)
    {
        var validator = new IndonesianPhoneNumberValidator();
        var validationResult = validator.Validate(s);
        if (result)
            Assert.True(validationResult.IsValid);
        else
            Assert.False(validationResult.IsValid);
    }

    /// <summary>
    /// Random number
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<object[]> GetDataTestPhoneNumberValidator()
    {
        yield return new object[] { "081111885422", true };
        yield return new object[] { "6281111885422", true };
        yield return new object[] { "+6281111885422", true };
        yield return new object[] { "+181885422", false };
    }
}