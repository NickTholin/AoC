
namespace AoC2024.Tests
{
    public class Day2Tests
    {
        [Fact]
        public void CalculateNumberOfSafeReportsTests()
        {
            var input = @"7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9";

            var safeReports = Day2.CalculateNumberOfSafeReports(input);

            safeReports.Should().Be(2);
        }
    }
}
