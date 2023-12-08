using System.Data;
using System.Runtime.InteropServices;

namespace Aoc.Core;

public static class DataFileReader
{
    
    static string GetFilename(int day, int? part=null)
    {
        const string dataFolder = "Data";
        var workingDir = Directory.GetCurrentDirectory();
        var separator = Path.DirectorySeparatorChar;
        var fileName= $"0{day}.txt".Replace("00", "0"); 
        var fileToRead =  $"{workingDir}{separator}{dataFolder}{separator}{fileName}";
        Console.WriteLine($"Reading data from {fileToRead}");
        return fileToRead;
    }
    
    public static string[] ReadFileAsLines(int day, int? part = null)
    {
        var data = File.ReadAllLines(GetFilename(day,part));
        Console.WriteLine($"Number of lines is {data.Length}");
        return data;
    }
}