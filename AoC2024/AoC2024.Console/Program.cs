﻿using AoC._2024;
using AoC_2024;

//Day 1
var input = File.ReadAllText(@"./input/day1.txt");

//var distance = Day1.FindTotalDistance(input);
//Console.WriteLine($"Distance: {distance}");

//var simularityScore = Day1.FindSimilarityScore(input);
//Console.WriteLine($"Simularity Score: {simularityScore}");

////Day2
//var input2 = File.ReadAllText(@"./input/day2.txt");

//var safeReports = Day2.CalculateNumberOfSafeReports(input2, false);
//Console.WriteLine($"Safe Reports: {safeReports}");

//var safeReportsWithDampener = Day2.CalculateNumberOfSafeReports(input2, true);
//Console.WriteLine($"Safe Reports w/ error dampener: {safeReportsWithDampener}");

////Day3
////var input3 = File.ReadAllText(@"./input/day3.txt");

////var sumOfMuls = AoC2024.Day3.CalculatedUncorruptedMulInstructions(input3);
////Console.WriteLine($"Sum of Muls: {sumOfMuls}");

////var sumOfMulsWithEnablement = AoC2024.Day3.CalculatedUncorruptedMulInstructionsWithEnablement(input3);
////Console.WriteLine($"Sum of Muls(enablement): {sumOfMulsWithEnablement}");

////Day4
//var input4 = File.ReadAllText(@"./input/day4.txt");

//var xMasCount = Day4.FindXmas(input4);
//Console.WriteLine($"Number of Xmas: {xMasCount}");

//var crossMasCount = Day4.FindCrossMas(input4);
//Console.WriteLine($"Number of CrossMas: {crossMasCount}");


//2015

//var input2015Day1 = File.ReadAllText(@"./2015/input/day1.txt");

//var level = AoC_2015.Day1.DetermineEndingFloor(input2015Day1);
//Console.WriteLine($"2015 - Day 1 - Part 1: {level}");

//var basementInstruction = AoC_2015.Day1.DetermineBasementInstrunction(input2015Day1);
//Console.WriteLine($"2015 - Day 1 - Part 2: {basementInstruction}");

//var input2024Day5 = File.ReadAllText(@"./2024/input/day5.txt");

//var sumOfMidpoints = Day5.FindSumOfMiddleOfPrintedPageNumbers(input2024Day5);
//Console.WriteLine($"2024 - Day 5 - Part 1: {sumOfMidpoints}");

//var sumOfFixedMidpoints = Day5.FindSumOfMiddleOfFixedPrintedPageNumbers(input2024Day5);
//Console.WriteLine($"2024 - Day 5 - Part 2: {sumOfFixedMidpoints}");

var input2024Day6 = File.ReadAllText(@"./2024/input/day6.txt");
var traversions = Day6.GetDistinctPositionsOfGuardPath(input2024Day6);
Console.WriteLine($"2024 - Day 6 - Part 1: {traversions}");

var numberOfBlockablePaths = Day6.GetNumberOfBlockablePaths(input2024Day6);
Console.WriteLine($"2024 - Day 6 - Part 2: {numberOfBlockablePaths}");



var input2024Day8 = File.ReadAllText(@"./2024/input/day8.txt");
var numAntenodes = Day8.NumberOfAntinodes(input2024Day8);
Console.WriteLine($"2024 - Day 8 - Part 2: {numAntenodes}");



Console.ReadKey();