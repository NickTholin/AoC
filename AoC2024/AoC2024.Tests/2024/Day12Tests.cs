using AoC._2024;

namespace AoC.Tests._2024;
public class Day12Tests
{
    [Fact]
    public void Day12Part1_TestSample1()
    {
        string input = @"
AAAA
BBCD
BBCC
EEEC";
        var cost = Day12.FindCostOfGardenFence(input);
        cost.Should().Be(140);
    }

    [Fact]
    public void Day12Part1_TestSample2()
    {
        string input = @"
RRRRIICCFF
RRRRIICCCF
VVRRRCCFFF
VVRCCCJFFF
VVVVCJJCFE
VVIVCCJJEE
VVIIICJJEE
MIIIIIJJEE
MIIISIJEEE
MMMISSJEEE";
        var cost = Day12.FindCostOfGardenFence(input);
        cost.Should().Be(1930);
    }

    [Fact]
    public void Day12Part2_TestSample1()
    {
        string input = @"
AAAA
BBCD
BBCC
EEEC";
        var cost = Day12.FindCostOfGardenFence(input);
        cost.Should().Be(80);
    }

    [Fact]
    public void Day12Part2_TestSample2()
    {
        string input = @"
OOOOO
OXOXO
OOOOO
OXOXO
OOOOO";
        var cost = Day12.FindCostOfGardenFence(input);
        cost.Should().Be(436);
    }

    [Fact]
    public void Day12Part2_TestSample3()
    {
        string input = @"
EEEEE
EXXXX
EEEEE
EXXXX
EEEEE";
        var cost = Day12.FindCostOfGardenFence(input);
        cost.Should().Be(236);
    }

    [Fact]
    public void Day12Part2_TestSample4()
    {
        string input = @"
AAAAAA
AAABBA
AAABBA
ABBAAA
ABBAAA
AAAAAA";
        var cost = Day12.FindCostOfGardenFence(input);
        cost.Should().Be(368);
    }

    [Fact]
    public void Day12Part2_TestSample5()
    {
        string input = @"
RRRRIICCFF
RRRRIICCCF
VVRRRCCFFF
VVRCCCJFFF
VVVVCJJCFE
VVIVCCJJEE
VVIIICJJEE
MIIIIIJJEE
MIIISIJEEE
MMMISSJEEE";
        var cost = Day12.FindCostOfGardenFence(input);
        cost.Should().Be(1206);
    }
}

