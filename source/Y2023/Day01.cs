using System.Text.RegularExpressions;

namespace Y2023;

public static class Day01
{
    private const string SingleDigit= @"\d";
    private const string SingleDigitAndDigitAsText = @"\d|one|two|three|four|five|six|seven|eight|nine";


    public static string Part1(string[] lines, bool debug = false)
    {
        var patternLeftToRight = new Regex(SingleDigit);
        var patternRightToLeft = new Regex(SingleDigit, RegexOptions.RightToLeft);
        
        if (lines == null) throw new ArgumentNullException(nameof(lines));
        
        var sum = 0;
        var lineNumber = 0;
        foreach (var line in lines)
        {
            lineNumber++;
            var firstDigit = "0"; 
            var lastDigit = "0";
            
            var firstMatch = patternLeftToRight.Match(line);
            var lastMatch = patternRightToLeft.Match(line);

            if (firstMatch.Success & lastMatch.Success)
            {
                firstDigit = firstMatch.Value;
                lastDigit = lastMatch.Value;
            }
            
            var number = Convert.ToInt32($"{firstDigit}{lastDigit}");
            if (debug) Console.WriteLine($"{lineNumber}:{firstDigit}{lastDigit}={number}:{line}");
            sum += number;
        }
        Console.WriteLine($"The sum for part 1 is {sum}");
        return sum.ToString();
    }
    
    
    public static string Part2(string[] lines, bool debug = false)
    {
        if (lines == null) throw new ArgumentNullException(nameof(lines));
        
        var patternLeftToRight = new Regex(SingleDigitAndDigitAsText);
        var patternRightToLeft = new Regex(SingleDigitAndDigitAsText, RegexOptions.RightToLeft);
        
        var sum = 0;
        var lineNumber = 0;
        
        foreach (var line in lines)
        {
            lineNumber++;
            var firstDigit = "0"; 
            var lastDigit = "0";
            
            var firstMatch = patternLeftToRight.Match(line);
            var lastMatch = patternRightToLeft.Match(line);

            if (firstMatch.Success & lastMatch.Success)
            {
                firstDigit = ReplaceWordWithNumber(firstMatch.Value);
                lastDigit = ReplaceWordWithNumber(lastMatch.Value);
            }
            
            var number = Convert.ToInt32($"{firstDigit}{lastDigit}");
            if (debug) Console.WriteLine($"{lineNumber}:{firstDigit}{lastDigit}={number}:{line}");
            sum += number;
        }
        Console.WriteLine($"The sum for part 2 is {sum}");
        return sum.ToString();    
    }

    private static string ReplaceWordWithNumber(string text)
    {
        const string s1 = "one";
        const string s2 = "two";
        const string s3 = "three";
        const string s4 = "four";
        const string s5 = "five";
        const string s6 = "six";
        const string s7 = "seven";
        const string s8 = "eight";
        const string s9 = "nine";
            
        text = text.Replace(s1,"1");
        text = text.Replace(s2,"2");
        text = text.Replace(s3,"3");
        text = text.Replace(s4,"4");
        text = text.Replace(s5,"5");
        text = text.Replace(s6,"6");
        text = text.Replace(s7,"7");
        text = text.Replace(s8,"8");
        text = text.Replace(s9,"9");
        return text;
    }
}
