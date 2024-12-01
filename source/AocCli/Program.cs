using Aoc.Core;

const int day = 1;
var data = DataFileReader.ReadFileAsLines(day, DataFileType.Real);
var result = Y2024.Day01.Part1(data);
Console.WriteLine($"Result {result}");