using System.Linq;
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

    private static IEnumerable<string> Reverse(IEnumerable<string> original)
    {
        return original.Select(x => new string(x.Reverse().ToArray()));
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
}
