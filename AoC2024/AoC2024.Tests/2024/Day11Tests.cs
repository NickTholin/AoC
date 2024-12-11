using AoC._2024;

namespace AoC.Tests._2024;

public class Day11Tests
{
    [Fact]
    public void Day11Part1_Test()
    {
        var input = @"0 1 10 99 999";

        var result = Day11.TotalNumberOfStonesAfterBlinking(input, 1);

        result.Should().Be(7);
    }

    [Fact]
    public void Day11Part1_Test2()
    {
        var input = @"125 17";

        var result = Day11.TotalNumberOfStonesAfterBlinking(input, 1);

        result.Should().Be(3);
    }

    [Fact]
    public void Day11Part1_Test3()
    {
        var input = @"125 17";

        var result = Day11.TotalNumberOfStonesAfterBlinking(input, 2);

        result.Should().Be(4);
    }

    [Fact]
    public void Day11Part1_Test4()
    {
        var input = @"125 17";

        var result = Day11.TotalNumberOfStonesAfterBlinking(input, 4);

        result.Should().Be(9);
    }

    [Fact]
    public void Day11Part1_Test5()
    {
        var input = @"125 17";

        var result = Day11.TotalNumberOfStonesAfterBlinking(input, 5);

        result.Should().Be(13);
    }
}
