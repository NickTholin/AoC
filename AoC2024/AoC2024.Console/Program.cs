using AoC;

//Day 1
var input = File.ReadAllText(@"./input/day1.txt");

var distance = Day1.FindTotalDistance(input);
Console.WriteLine($"Distance: {distance}");

var simularityScore = Day1.FindSimilarityScore(input);
Console.WriteLine($"Simularity Score: {simularityScore}");

//Day2
var input2 = File.ReadAllText(@"./input/day2.txt");

var safeReports = Day2.CalculateNumberOfSafeReports(input2, false);
Console.WriteLine($"Safe Reports: {safeReports}");

var safeReportsWithDampener = Day2.CalculateNumberOfSafeReports(input2, true);
Console.WriteLine($"Safe Reports w/ error dampener: {safeReportsWithDampener}");

//Day3
//var input3 = File.ReadAllText(@"./input/day3.txt");

//var sumOfMuls = AoC2024.Day3.CalculatedUncorruptedMulInstructions(input3);
//Console.WriteLine($"Sum of Muls: {sumOfMuls}");

//var sumOfMulsWithEnablement = AoC2024.Day3.CalculatedUncorruptedMulInstructionsWithEnablement(input3);
//Console.WriteLine($"Sum of Muls(enablement): {sumOfMulsWithEnablement}");

//Day4
var input4 = File.ReadAllText(@"./input/day4.txt");

var xMasCount = Day4.FindXmas(input4);
Console.WriteLine($"Number of Xmas: {xMasCount}");

var crossMasCount = Day4.FindCrossMas(input4);
Console.WriteLine($"Number of CrossMas: {crossMasCount}");

Console.ReadKey();