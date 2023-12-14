using System.Data;
using System.Runtime.InteropServices;

namespace Aoc.Core;

public static class DataFileReader
{
    
    private static string GetFilename(int day, int? part = 0, bool? example = false)
    {
        const string sDataFolder = "Data";
        var workingDir = Directory.GetCurrentDirectory();
        var separator = Path.DirectorySeparatorChar;
        var fileName= $"0{day}.txt".Replace("00", "0");
        if (part > 0)
        {
            fileName = fileName.Replace(".txt",$"-{part}.txt");
        }
        if (example.HasValue & example.GetValueOrDefault())
        {
            fileName = fileName.Replace(".txt", "-example.txt");
        }
        var fileToRead =  $"{workingDir}{separator}{sDataFolder}{separator}{fileName}";
        Console.WriteLine($"Reading data from {fileToRead}");
        return fileToRead;
    }
    
    public static string[] ReadFileAsLines(int day, int? part = 0, bool? example = false)
    {
        var data = File.ReadAllLines(GetFilename(day,part, example));
        Console.WriteLine($"There are {data.Length} lines");
        return data;
    }
    
    public static char[,] ReadFileAsArray(int day, int? part = 0, bool? example = false)
    {
        var lines = ReadFileAsLines(day, part, example);
        var data = new char[lines[0].Length,lines.Length];
        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[0].Length; col++)
            {
                data[row, col] = lines[row][col];
            }    
        }
        return data;
    }
    
    public static string ReadFileAsString(int day, int? part = 0, bool? example = false)
    {
        var data = File.ReadAllText(GetFilename(day,part, example));
        Console.WriteLine($"There length of the text is  {data.Length} characters");
        return data;
    }
}