using AoC._2024;

namespace AoC.Tests._2024;

public class Day6Tests
{
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

        var traversions = Day6.GetDistinctPositionsOfGuardPath(input);
        traversions.Should().Be(41);
    }
}
