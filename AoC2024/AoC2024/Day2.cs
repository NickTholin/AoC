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
                    .Select(y => int.Parse(y))
                    .ToList());

            var safeReports = 0;

            var isDecreasing = (int x, int y) => x - y > 0;
            var isIncreasing = (int x, int y) => x - y < 0;
            var isInRange = (int x, int y) => Math.Abs(x - y) <= 3;

            foreach (var report in reportsList)
            {
                Trend trend;
                var isSafe = true;

                if (report[0] < report[1])
                    trend = Trend.Increasing;
                else if (report[0] > report[1])
                    trend = Trend.Decreasing;
                else
                    continue; //unsafe because levels don't increase or decrease.

                for (int i = 0; i < report.Count - 1; i++)
                {
                    var current = report[i];
                    var next = report[i + 1];

                    if (trend == Trend.Decreasing && isDecreasing(current, next) && isInRange(current, next)) continue;
                    if (trend == Trend.Increasing && isIncreasing(current, next) && isInRange(current, next)) continue;

                    isSafe = false;
                    break;
                }

                if (isSafe)
                    safeReports++;
            }

            return safeReports;
        }


    }
}
