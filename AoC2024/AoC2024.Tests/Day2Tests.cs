
namespace AoC2024.Tests
{
    public class Day2Tests
    {
        [Theory]
        [InlineData(false, 2)]
        [InlineData(true, 10)]
        public void CalculateNumberOfSafeReportsTests(bool useErrorDampener, int expectedSafeReports)
        {
            var input = @"7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9
7 7 6 5 4
7 8 6 5 3
7 5 3 2 3
2 7 8 9 10
9 8 4 6 3
1 3 2 1";

            var safeReports = Day2.CalculateNumberOfSafeReports(input, useErrorDampener);

            safeReports.Should().Be(expectedSafeReports);
        }
    }
}
