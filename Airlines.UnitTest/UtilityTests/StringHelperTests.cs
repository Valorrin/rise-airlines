﻿using Airlines.Business.Utilities;

namespace Airlines.UnitTests.UtilityTests;

[Collection("Sequential")]
public class StringHelperTests
{
    [Fact]
    public void SplitBeforeLastElement_InputWithSpace_ReturnsCorrectParts()
    {
        var input = "Hello World";
        var result = StringHelper.SplitBeforeLastElement(input);
        Assert.Equal("Hello", result[0]);
        Assert.Equal("World", result[1]);
    }
}