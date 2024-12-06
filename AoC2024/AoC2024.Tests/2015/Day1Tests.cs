namespace AoC.Tests._2015;

public class Day1Tests
{
    [Theory]
    [InlineData("(())", 0)]
    [InlineData("()()", 0)]
    [InlineData("(((", 3)]
    [InlineData("(()(()(", 3)]
    [InlineData("))(((((", 3)]
    [InlineData("())", -1)]
    [InlineData("))(", -1)]
    [InlineData(")))", -3)]
    [InlineData(")())())", -3)]
    public void FloorDetector_Tests(string input, int expectedLevel)
    {
        var actual = AoC_2015.Day1.DetermineEndingFloor(input);

        actual.Should().Be(expectedLevel);
    }
}
