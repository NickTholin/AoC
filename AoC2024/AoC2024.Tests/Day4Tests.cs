namespace AoC2024.Tests
{
    public class Day4Tests
    {
        [Fact]
        public void CrossWordTest()
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
    }
}
