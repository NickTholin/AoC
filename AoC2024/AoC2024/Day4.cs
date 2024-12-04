using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AoC2024;
public class Day4
{
    public static int FindXmas(string input)
    {
        List<string> wordSearch = input.Split(Environment.NewLine).ToList();
        var WordToSearch = "XMAS";
        var reverseXmas = "SAMX";

        //Why search grid when you can transform it? ¯\_(ツ)_/¯
        List<string> allTransformations = new List<string>(wordSearch);

        var rightDiagonals = GetRightDiagonals(wordSearch);
        var upDown = GetUpDownWordSearch(wordSearch);
        var leftDiagonals = GetLeftDiagonals(wordSearch);

        allTransformations = allTransformations
        .Concat(upDown)
        .Concat(leftDiagonals)
        .Concat(rightDiagonals).ToList();

        return allTransformations.Sum(x => x.Split(WordToSearch).Length -1 + x.Split(reverseXmas).Length - 1); // -1 for each tail after each split with no match.
    }

    public static int FindCrossMas(string input)
    {
        List<string> wordSearch = input.Split(Environment.NewLine).ToList();

        var width = wordSearch[0].Length - 1;
        var height = wordSearch.Count - 1;

        var allAs = FindAllAs(wordSearch, height, width);
        var crossMasCount = countAllCrossMas(wordSearch, allAs, height, width);

        return crossMasCount;
    }

    private static List<Point> FindAllAs(List<string> wordSearch, int height, int width)
    {
        var aLocations = new List<Point>();

        for (var x = 0; x <= width; x++)
        {
            for (var y = 0; y <= height; y++)
            {
                //grid is accessed first array as height, so y,x Will leave points as x,y as standard axis
                if (wordSearch[y][x] == 'A')
                    aLocations.Add(new Point(x, y));
            }
        }

        return aLocations;
    }

    private static int countAllCrossMas(List<string> wordSearch, List<Point> points, int height, int width)
    {
        var masCount = 0;
        foreach (var a in points)
        {
            var topLeft = new Point(a.X - 1, a.Y - 1);
            var topRight = new Point(a.X + 1, a.Y - 1);
            var bottomLeft =new Point(a.X - 1, a.Y + 1);
            var bottomRight = new Point(a.X + 1, a.Y + 1);

            if (topLeft.IsOutOfBounds(height, width) || 
                topRight.IsOutOfBounds(height, width) ||
                bottomLeft.IsOutOfBounds(height, width) || 
                bottomRight.IsOutOfBounds(height, width))
            {
                continue;
            }

            if (((wordSearch[topLeft.Y][topLeft.X] == 'M' && wordSearch[bottomRight.Y][bottomRight.X] == 'S') ||
                (wordSearch[topLeft.Y][topLeft.X] == 'S' && wordSearch[bottomRight.Y][bottomRight.X] == 'M')) &&
                ((wordSearch[topRight.Y][topRight.X] == 'M' && wordSearch[bottomLeft.Y][bottomLeft.X] == 'S') ||
                (wordSearch[topRight.Y][topRight.X] == 'S' && wordSearch[bottomLeft.Y][bottomLeft.X] == 'M')))
            {
                masCount++;
            }
        }

        return masCount;
    }

    private static IEnumerable<string> GetUpDownWordSearch(List<string> original)
    {
        var updownLists = new List<string>();

        for (int i = 0; i < original.Count; i++)
        {
            var updownLine = new StringBuilder();

            foreach (var line in original)
            {
                updownLine.Append(line[i]);
            }
            updownLists.Add(updownLine.ToString());
        }

        return updownLists;
    }

    private static IEnumerable<string> GetRightDiagonals(List<string> original)
    {
        var width = original.First().Length;
        var height = original.Count;

        List<string> lines = new List<string>();

        for (int i = 0; i < width; i++)
        {
            var line = new StringBuilder();

            line.Append(original[0][i]);
            
            for (int j = 1; j <= height && i-j >= 0; j++)
            {
                line.Append(original[j][i-j]);
            }

            lines.Add(line.ToString());
        }

        var lastWidthIndex = width - 1;
        for (int i = 1; i< height; i++)
        {
            var line = new StringBuilder();
            line.Append(original[i][lastWidthIndex]);

            //this is the only way I could get this abomination to work. original idea was fun but this proves it was wasted time.
            for (int j = i + 1, k = 1;  j < height; j++, k++)
            {
                line.Append(original[j][lastWidthIndex - k]);
            }

            lines.Add(line.ToString());
        }

        return lines;
    }

    private static IEnumerable<string> GetLeftDiagonals(List<string> original)
    {
        var width = original.First().Length;
        var height = original.Count;

        List<string> lines = new List<string>();

        for (int i = 0; i < width; i++)
        {
            var line = new StringBuilder();

            line.Append(original[0][i]);

            for (int j = 1; j <= height && i + j < width; j++)
            {
                line.Append(original[j][i + j]);
            }

            lines.Add(line.ToString());
        }

        var lastWidthIndex = width - 1;
        for (int i = 1; i < height; i++)
        {
            var line = new StringBuilder();
            line.Append(original[i][0]);

            for (int j = i + 1; j < height; j++)
            {
                line.Append(original[j][j - i]);
            }

            lines.Add(line.ToString());
        }

        return lines;
    }


    public record struct Point(int X, int Y)
    {
        public bool IsOutOfBounds(int height, int width)
        {
            return (X < 0 || X > width || Y < 0 || Y > height);
        }
    }
}
