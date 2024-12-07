using AoC._2024;

namespace AoC.Tests._2024;

public class Day6Tests
{
    [Fact]
    public void Part1Test()
    {
        string input = @"....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...";

        var traversions = Day6.GetDistinctPositionsOfGuardPath(input);
        traversions.Should().Be(41);
    }

    [Fact]
    public void Part2Test()
    {
        string input = @"....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...";

        var cycleInducingPaths = Day6.GetNumberOfBlockablePaths(input);
        cycleInducingPaths.Should().Be(6);
    }

    [Fact]
    public void Part2Test2()
    {
        string input = @"
....
#..#
.^#.";

        var cycleInducingPaths = Day6.GetNumberOfBlockablePaths(input);
        cycleInducingPaths.Should().Be(1);
    }

    [Fact]
    public void Part2Test3()
    {
        string input = @"
.##..
....#
.....
.^.#.
.....";

        var cycleInducingPaths = Day6.GetNumberOfBlockablePaths(input);
        cycleInducingPaths.Should().Be(1);
    }

    [Fact]
    public void Part2Test4()
    {
        string input = @"
....
#...
.^#.
.#..";

        var cycleInducingPaths = Day6.GetNumberOfBlockablePaths(input);
        cycleInducingPaths.Should().Be(0);
    }

    [Fact]
    public void Part2Test5()
    {
        string input = @"
.#..
#..#
....
^...
#...
.#..";

        var cycleInducingPaths = Day6.GetNumberOfBlockablePaths(input);
        cycleInducingPaths.Should().Be(1);
    }

    [Fact]
    public void Part2Test6()
    {
        string input = @"
.#....
.....#
...^..
#.....
#.....
....#.";

        var cycleInducingPaths = Day6.GetNumberOfBlockablePaths(input);
        cycleInducingPaths.Should().Be(1);
    }


    [Fact]
    public void Part2Test7()
    {
        string input = @"
.##.
#..#
....
..^.";

        var cycleInducingPaths = Day6.GetNumberOfBlockablePaths(input);
        cycleInducingPaths.Should().Be(0);
    }
}
