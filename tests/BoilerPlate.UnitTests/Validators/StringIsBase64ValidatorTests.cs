﻿using Shouldly;

namespace BoilerPlate.UnitTests.Validators;

public class StringIsBase64ValidatorTests
{
    [Theory]
    [ClassData(typeof(TestDataStringIsBase64))]
    public void TestStringIsBase64Validator(string s, bool result)
    {
        var validator = new StringIsBase64Validator();
        var validationResult = validator.Validate(s);

        if (result)
            validationResult.IsValid.ShouldBeTrue();
        else
            validationResult.IsValid.ShouldBeFalse();
    }
}