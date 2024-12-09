using AoC._2024;

namespace AoC.Tests._2024
{
    public class Day9Tests
    {

        [Fact]
        public void DayPart1Expand()
        {
            string input = @"12345";

            var num = Day9.CompressForAmphipod(input);
            num.Should().Be("0..111....22222");
        }
    }
}
