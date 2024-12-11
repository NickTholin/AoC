using System.Numerics;

namespace AoC._2024;
public class Day7
{
    public static long SumWorkableEquations(string input, bool useThirdOperator = false)
    {
        var equationLines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var equationsPartitioned = equationLines
            .Select(x => x.Split(":"))
            .Select(x => (total: long.Parse(x[0]), numbers: x[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)));

        var total = 0L;

        foreach (var equation in equationsPartitioned)
        {
            var workableEquations = DoesEquationWork(equation.total, 0, 0, equation.numbers.ToArray(), useThirdOperator);
            if (workableEquations)
                total += equation.total;
        }
        return total;
    }

    private static bool DoesEquationWork(long targetTotal, long currentTotal, int currentIndex, int[] nums, bool useThirdOperator)
    {
        var found = false;
        if (currentIndex <= nums.Length - 1)
        {
            var operand2 = nums[currentIndex];
            currentIndex++;

            found = DoesEquationWork(targetTotal,currentTotal + operand2, currentIndex, nums, useThirdOperator);
            found = found || DoesEquationWork(targetTotal, currentTotal * operand2, currentIndex, nums, useThirdOperator);
            if (useThirdOperator)
            {
                found = found || DoesEquationWork(targetTotal, long.Parse(string.Concat(currentTotal, operand2.ToString())), currentIndex, nums, useThirdOperator);
            }

            return found;
        }
        else
        {
            return currentTotal == targetTotal;
        }
    }
}
