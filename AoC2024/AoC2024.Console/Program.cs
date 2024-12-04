//Day 1
var input = File.ReadAllText(@"./input/day1.txt");

var distance = AoC2024.Day1.FindTotalDistance(input);
Console.WriteLine($"Distance: {distance}");

var simularityScore = AoC2024.Day1.FindSimilarityScore(input);
Console.WriteLine($"Simularity Score: {simularityScore}");

//Day2
var input2 = File.ReadAllText(@"./input/day2.txt");

var safeReports = AoC2024.Day2.CalculateNumberOfSafeReports(input2, false);
Console.WriteLine($"Safe Reports: {safeReports}");

var safeReportsWithDampener = AoC2024.Day2.CalculateNumberOfSafeReports(input2, true);
Console.WriteLine($"Safe Reports w/ error dampener: {safeReportsWithDampener}");

Console.ReadKey();