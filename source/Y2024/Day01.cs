using System.Data.Common;
using System.Text.RegularExpressions;
using Aoc.Core;

namespace Y2024;

public class Day01
{
    public static string Part1(string[] data, bool debug = false)
    {
        if (data == null) throw new ArgumentNullException(nameof(data));

        var patternLeftToRight = new Regex(Constants.RegxSingleDigit);
        var patternRightToLeft = new Regex(Constants.RegxSingleDigit, RegexOptions.RightToLeft);
        var list1 = new int[data.Length];
        var list2 = new int[data.Length];
        for (var i = 0; i < data.Length; i++)
        {
            list1[i] = Convert.ToInt32(patternLeftToRight.Match(data[i]).Value);
            list2[i] = Convert.ToInt32(patternRightToLeft.Match(data[i]).Value);
        }

        Console.WriteLine("Order lists");
        var sortedLeft = list1.OrderBy(x => x).ToArray();
        var sortedRight = list2.OrderBy(x => x).ToArray();

        Console.WriteLine("Calculate distance");
        var totalDistance = 0;
        for (int i = 0; i < data.Length; i++)
        {
            var distance = sortedLeft[i] - sortedRight[i];
            if (distance < 0) distance *= -1;
            totalDistance += distance;
            if (debug) Console.WriteLine($"{i}: { sortedLeft[i]}-{ sortedRight[i]}={distance} -> {totalDistance}");
        }

        Console.WriteLine($"Total distance {totalDistance}");
        return totalDistance.ToString();
    }
    
    
    public static string Part2(string[] data, bool debug = false)
    {
        if (data == null) throw new ArgumentNullException(nameof(data));

        var patternLeftToRight = new Regex(Constants.RegxSingleDigit);
        var patternRightToLeft = new Regex(Constants.RegxSingleDigit, RegexOptions.RightToLeft);
        var list1 = new int[data.Length];
        var list2 = new int[data.Length];
        for (var i = 0; i < data.Length; i++)
        {
            list1[i] = Convert.ToInt32(patternLeftToRight.Match(data[i]).Value);
            list2[i] = Convert.ToInt32(patternRightToLeft.Match(data[i]).Value);
        }

        Console.WriteLine("Order lists");
        var sortedLeft = list1.OrderBy(x => x).ToArray();
        var sortedRight = list2.OrderBy(x => x).ToArray();

        Console.WriteLine("Calculate similarity score");
        var totalSimilarityScore = 0;
        for (int i = 0; i < data.Length; i++)
        {
            var number = sortedLeft[i];
            var occurrences = sortedRight.Where(x => x == number).ToArray().Length;
            var similarityScore = number * occurrences;
            totalSimilarityScore += similarityScore;
            if (debug) Console.WriteLine($"{i}: { sortedLeft[i]}*{occurrences}={similarityScore} -> {totalSimilarityScore}");
        }

        Console.WriteLine($"Total similarity score {totalSimilarityScore}");
        return totalSimilarityScore.ToString();
    }
    
}