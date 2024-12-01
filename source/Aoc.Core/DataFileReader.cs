using System.Data;
using System.Runtime.InteropServices;

namespace Aoc.Core;

public static class DataFileReader
{
    private static string GetFilename(int day, int? part = null, bool? example = false)
    {
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
        var fileToRead =  $"{workingDir}{separator}{Constants.DataFolder}{separator}{fileName}";
        Console.WriteLine($"Reading data from {fileToRead}");
        return fileToRead;
    }
    
    public static string[] ReadFileAsLines(int day, DataFileType dataSetType)
    {
        var data = dataSetType switch
        {
            DataFileType.Example => File.ReadAllLines(GetFilename(day, null, true)),
            DataFileType.Example1 => File.ReadAllLines(GetFilename(day, 1, true)),
            DataFileType.Example2 => File.ReadAllLines(GetFilename(day, 2, true)),
            DataFileType.Real => File.ReadAllLines(GetFilename(day, null, false)),
            _ => throw new ArgumentOutOfRangeException(nameof(dataSetType), dataSetType, null)
        };
        Console.WriteLine($"There are {data.Length} lines");
        return data;
    }
    
    public static char[,] ReadFileAsArray(int day, DataFileType dataSetType)
    {
        var lines = ReadFileAsLines(day, dataSetType);
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
    
    public static string ReadFileAsString(int day,DataFileType dataSetType)
    {
        string data = dataSetType switch
        {
            DataFileType.Example => File.ReadAllText(GetFilename(day, null, true)),
            DataFileType.Example1 => File.ReadAllText(GetFilename(day, 1, true)),
            DataFileType.Example2 => File.ReadAllText(GetFilename(day, 2, true)),
            DataFileType.Real => File.ReadAllText(GetFilename(day, null, false)),
            _ => throw new ArgumentOutOfRangeException(nameof(dataSetType), dataSetType, null)
        };
        Console.WriteLine($"There length of the text is  {data.Length} characters");
        return data;
    }
}