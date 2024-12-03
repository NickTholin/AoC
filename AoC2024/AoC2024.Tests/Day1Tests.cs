using FluentAssertions;

namespace AoC2024.Tests
{
    public class Day1Tests
    {
        [Fact]
        public void CalculateDistanceBetweenPointsTest()
        {
            var expectedDistance = 11;

            string input = @"
3    4
4    3
2    5
1    3
3    9
3    3
";
            var distance = Day1.FindTotalDistance(input);

            distance.Should().Be(expectedDistance);
        }

        [Fact]
        public void CalculateSimilarityScoreTest()
        {
            var expectedDistance = 31;

            string input = @"
3   4
4   3
2   5
1   3
3   9
3   3
";
            var distance = Day1.FindSimilarityScore(input);

            distance.Should().Be(expectedDistance);
        }
    }
}