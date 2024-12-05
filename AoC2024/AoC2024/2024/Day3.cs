using System.Text.RegularExpressions;
namespace AoC;

public class Day3
{
    /// <summary>
    /// Parse and sum of multiplication of all instances of `mul({ddd},{ddd})`
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static int CalculatedUncorruptedMulInstructions(string input)
    {
        var pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        var regex = new Regex(pattern);

        var muls = regex.Matches(input);

        var sum = 0;
        foreach(Match mul in muls)
        {
            var operand1 = int.Parse(mul.Groups[1].Value);
            var operand2 = int.Parse(mul.Groups[2].Value);
            sum += operand1 * operand2;
        }

        return sum;
    }

    /// <summary>
    /// Parse and sum of multiplication of all instances of `mul({ddd},{ddd})`
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static int CalculatedUncorruptedMulInstructionsWithEnablement(string input)
    {
        const string DO = "do()";
        const string DONT = "don't()";

        var mulPattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        var doPattern = @"do\(\)";
        var dontPattern = @"don't\(\)";
        var regex = new Regex($"{mulPattern}|{doPattern}|{dontPattern}");

        var matches = regex.Matches(input);

        var sum = 0;
        var mulEnabled = true;

        foreach (Match match in matches)
        {
            if (match.Value == DO)
            {
                mulEnabled = true;
                continue;
            }
            if (match.Value == DONT)
            {
                mulEnabled = false;
                continue;
            }

            if (mulEnabled)
            {
                var operand1 = int.Parse(match.Groups[1].Value);
                var operand2 = int.Parse(match.Groups[2].Value);
                sum += operand1 * operand2;
            }
        }

        return sum;
    }
}
