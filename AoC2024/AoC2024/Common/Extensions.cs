namespace AoC.Common;

public static class Extensions
{
    public static T[][] To2DArray<T>(this string input, Func<string, T> converter, char? delimiter = null)
    {
        return input
            .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
            .Select(line =>
                delimiter.HasValue && line.Contains(delimiter.Value)
                    ? line.Split(delimiter.Value).Select(converter).ToArray()
                    : line.Select(c => converter(c.ToString())).ToArray()
            )
            .ToArray();
    }

    public static List<(int x, int y)> FindCoordinates<T>(this T[][] array, T target)
    {
        var coordinates = new List<(int x, int y)>();
        for (int y = 0; y < array.Length; y++) // "Y" is the row (vertical axis)
        {
            for (int x = 0; x < array[y].Length; x++) // "X" is the column (horizontal axis)
            {
                if (EqualityComparer<T>.Default.Equals(array[y][x], target))
                {
                    coordinates.Add((x, y)); // Add coordinate in (x, y) format
                }
            }
        }
        return coordinates;
    }

    public static List<(int x, int y)> FindCoordinatesOfMatchingValueAdjacentToCoordinate<T>(this T[][] array, (int x, int y) coordinate, T target, bool includeDiagonals = false)
    {
        var directions = includeDiagonals
            ? new (int dx, int dy)[] { (-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1) } // All 8 directions
            : new (int dx, int dy)[] { (0, -1), (-1, 0), (1, 0), (0, 1) }; // Cardinal directions only (up, down, left, right)

        var matchingCoordinates = new List<(int x, int y)>();
        var rowCount = array.Length; // Total number of rows (y-axis)

        for (int i = 0; i < directions.Length; i++)
        {
            var newX = coordinate.x + directions[i].dx;
            var newY = coordinate.y + directions[i].dy;

            // Check if newX and newY are within array bounds
            if (newY >= 0 && newY < rowCount && newX >= 0 && newX < array[newY].Length)
            {
                if (EqualityComparer<T>.Default.Equals(array[newY][newX], target))
                {
                    matchingCoordinates.Add((newX, newY));
                }
            }
        }

        return matchingCoordinates;
    }
}
