namespace AoC_2024;

/// <summary>
/// https://adventofcode.com/2024/day/1
/// </summary>
public class Day1
{
    /// <summary>
    /// Calculates sum of delta between sorted points of space delimited location pairs
    /// </summary>
    /// <param name="twoLists">Expected Input:
    /// numleft1    numright1
    /// numleft2    numright2
    /// Sum of delta of |numleft1 - numright2|</param>
    /// <returns>Sum of distance between each sorted list element</returns>
    public static int FindTotalDistance(string twoLists)
    {
        var (left, right) = ParseLocations(twoLists);
        left.Sort();
        right.Sort();

        var sum = 0;

        for(int i = 0; i <= left.Count -1; i++)
        {
            sum += Math.Abs(left[i] - right[i]);
        }

        return sum;
    }

    /// <summary>
    /// Calculations score based on sum of number in first list times occurrences in second list.
    /// </summary>
    /// <param name="twoLists"></param>
    /// <returns></returns>
    public static int FindSimilarityScore(string twoLists)
    {
        var (left, right) = ParseLocations(twoLists);
        
        //projecting to map keeps us from O=N^2.
        var rightMap = right.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

        var sum = 0;

        foreach (var location in left)
        {
            sum += location * rightMap.GetValueOrDefault(location);
        }

        return sum;
    }

    private static (List<int> Left, List<int> Right) ParseLocations(string twoLists)
    {
        var lines = twoLists.Split(Environment.NewLine);
        var allLocations = lines
            .SelectMany(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries))
            .Select(int.Parse);
        var left = allLocations.Where((x, i) => i % 2 == 0).ToList();
        var right = allLocations.Where((x, i) => i % 2 != 0).ToList();
        
        return (left, right);
    }

}
