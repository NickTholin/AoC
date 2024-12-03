namespace AoC2024
{
    /// <summary>
    /// https://adventofcode.com/2024/day/2
    /// </summary>
    public class Day2
    {
        public enum Trend
        {
            Increasing,
            Decreasing
        }

        public static int CalculateNumberOfSafeReports(string reports)
        {
            // transform string grid to IEnumerable<List<int>> 
            var reportsList = reports
                .Split(Environment.NewLine)
                .Select(x => x.Split(" "))
                .Select(x => x
                    .Select(int.Parse)
                    .ToList());

            return reportsList.Count(IsReportSafe);
        }

        public static bool IsReportSafe(List<int> report)
        {
            Trend trend;

            if (report[0] < report[1])
                trend = Trend.Increasing;
            else if (report[0] > report[1])
                trend = Trend.Decreasing;
            else
                return false; //unsafe because levels don't increase or decrease.

            for (var i = 0; i < report.Count - 1; i++)
            {
                var current = report[i];
                var next = report[i + 1];

                if (IsNextLevelSafe(next, current, trend))
                    continue;

                return false;
            }

            return true;
        }

        public static bool IsNextLevelSafe(int nextLevel, int currentLevel, Trend expectedTrend)
        {
            bool IsDecreasing(int x, int y) => x - y > 0;
            bool IsIncreasing(int x, int y) => x - y < 0;
            bool IsInRange(int x, int y) => Math.Abs(x - y) <= 3 && x - y != 0; // [-3,0)(0, 3]
            bool DoesFollowTrend(Trend trend, int x, int y) => trend == Trend.Decreasing ? IsDecreasing(x, y) : IsIncreasing(x, y);
            
            return IsInRange(currentLevel, nextLevel) && DoesFollowTrend(expectedTrend, currentLevel, nextLevel);
        }
    }
}
