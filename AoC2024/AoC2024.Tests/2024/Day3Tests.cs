namespace AoC.Tests;
public class Day3Tests
{
    [Fact]
    public void Test()
    {
        string input = @"xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

        var sum = Day3.CalculatedUncorruptedMulInstructions(input);

        sum.Should().Be(161);
    }

    [Fact]
    public void Test_WithEnablement()
    {
        string input = @"xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

        var sum = Day3.CalculatedUncorruptedMulInstructionsWithEnablement(input);

        sum.Should().Be(48);
    }
}
