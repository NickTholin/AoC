namespace AoC;

/// <summary>
/// https://adventofcode.com/2024/day/2
/// </summary>
public class Day2
{
    public enum Trend
    {
        Increasing,
        Decreasing,
        Equal
    }

    private static bool UseErrorDampener;

    public static int CalculateNumberOfSafeReports(string reports, bool useErrorDampener)
    {
        UseErrorDampener = useErrorDampener;

        // transform string grid to IEnumerable<List<int>> 
        var reportsList = reports
            .Split(Environment.NewLine)
            .Select(x => x.Split(" "))
            .Select(x => x
                .Select(int.Parse).ToArray());

        return reportsList.Count(x => IsReportSafe(x, false));
    }

    public static bool IsReportSafe(int[] report, bool engageErrorDampener)
    {
        Trend trend;

        if (report[0] < report[1])
            trend = Trend.Increasing;
        else if (report[0] > report[1])
            trend = Trend.Decreasing;
        else
            trend = Trend.Equal;

        var unsafeLevelIndex = -1;
        var isSafe = true;

        for (var i = 0; i < report.Length - 1; i++)
        {
            var current = report[i];
            var next = report[i + 1];

            if (IsNextLevelSafe(next, current, trend)) continue;

            isSafe = false;
            unsafeLevelIndex = i;
            break;
        }

        if (isSafe) return true;
        if (engageErrorDampener) return false;

        return EngageErrorDampenerAndRetry(report, unsafeLevelIndex);
    }

    public static bool EngageErrorDampenerAndRetry(int[] report, int indexOfError)
    {
        if (!UseErrorDampener) return false;

        //If the error is at the end of the report then the report is safe, because we can eliminate just that level.
        // -2 to account for the 0 based index adjustment and the current algo is verifying the next value not current. 
        if (indexOfError == report.Length - 2) return true;


        //if the second level is causing an issue then the problem could be the first level.
        int[] reportWithFirstRemoved = [.. report[1..]];
        var reportWithCurrentRemoved = report.Where((source, index) => index != indexOfError).ToArray();
        var reportWithNextRemoved = report.Where((source, index) => index != indexOfError + 1).ToArray();


        return IsReportSafe(reportWithFirstRemoved, true) ||
               IsReportSafe(reportWithCurrentRemoved, true) ||
               IsReportSafe(reportWithNextRemoved, true);
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
