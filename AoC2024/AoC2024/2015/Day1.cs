namespace AoC_2015;

public class Day1
{
    public static int DetermineEndingFloor(string input)
    {
        var valueLookup = new Dictionary<char, int>() 
        {
            ['('] = 1,
            [')'] = -1 
        } ;

        return input.Aggregate(0, (int accumulator, char c) => accumulator += valueLookup[c]);
    }

    public static int DetermineBasementInstrunction(string input)
    {
        var valueLookup = new Dictionary<char, int>()
        {
            ['('] = 1,
            [')'] = -1
        };

        var basementInstructionPosition = -1;
        var currentLevel = 0;

        for (int i = 0; i < input.Length; i++)
        {
            currentLevel += valueLookup[input[i]];
            if (currentLevel == -1)
            {
                basementInstructionPosition = i + 1;
                break;
            }
        }

        return basementInstructionPosition;
    }
}
