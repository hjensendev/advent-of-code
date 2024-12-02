using Aoc.Core;

const int day = 2;
var data = DataFileReader.ReadFileAsLines(day, DataFileType.Real);
var result = Y2024.Day02.Part2(data,true);
Console.WriteLine($"Result {result}");