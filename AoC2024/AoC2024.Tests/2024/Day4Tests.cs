namespace AoC.Tests;

    public class Day4Tests
{
    [Fact]
    public void CrossWordTestXMas()
    {
        string input = @"MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX";

        var sum = Day4.FindXmas(input);

        sum.Should().Be(18);
    }

    [Fact]
    public void CrossWordTestCrossMas()
    {
        string input = @"MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX";

        var sum = Day4.FindCrossMas(input);

        sum.Should().Be(9);
    }
}
