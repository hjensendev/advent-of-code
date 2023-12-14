namespace Y2023;

public static class Day05
{
    private static bool _debug;
    
    public static string Part1(string text, bool debug = false)
    {
        _debug = debug;

        var almanac = ParseText(text);

        var result = 0;
        Console.WriteLine($"Result: {result}");
        return result.ToString();
    }
    
    
    
    public static string Part2(string text, bool debug = false)
    {
        _debug = debug;
        
        var result = 0;
        Console.WriteLine($"Result: {result}");
        return result.ToString();

    }

    public static Almanac ParseText(string text)
    {
        return new Almanac();
    }

    public class Almanac
    {
        
    }
}