using System.Text.RegularExpressions;
using Aoc.Core;

namespace Y2024;

public partial class Day03
{
    private const string DontConstant = "don't()";
    private const string DoConstant = "do()";
    public static string Part1(string data, bool debug = false)
    {
        ArgumentNullException.ThrowIfNull(data);

        var sum = 0;
        var matches = Mul().Matches(data);
        
        foreach (Match match in matches)
        {
            sum += Calculator.Mul($"{match}",debug);
        }

        Console.WriteLine($"Sum: {sum}");
        return sum.ToString();
    }
    
    
    public static string Part2(string data, bool debug = false)
    {
        ArgumentNullException.ThrowIfNull(data);

        var dataList = new List<string>();

        var firstDo= GetFirstData(data);
        var index = firstDo.nextIndex;
        dataList.Add(firstDo.data);

        
        var moreData = true;        
        while (moreData)
        {
            var (next,middleData) = GetMiddleData(data, index);
            if (string.IsNullOrEmpty(middleData))
            {
                moreData = false;
            }
            else
            {
                dataList.Add(middleData);
            }
            index = next;
        }
        
        
        var lastDo = GetLastData(data, index);
        dataList.Add(lastDo.data);

        var sum = dataList
            .SelectMany(processableData 
                => Mul().Matches(processableData))
            .Sum<object>(match 
                => Calculator.Mul($"{match}", debug));

        Console.WriteLine($"Sum: {sum}");
        return sum.ToString();
    }

    private static class Calculator
    {
        public static int Mul(string data, bool debug = false)
        {
            var numbers = new Regex(Constants.RegExSingleNumber).Matches(data);
            var i1 = Convert.ToInt32(numbers[0].Value);
            var i2 = Convert.ToInt32(numbers[1].Value);
            var result = (i1 * i2);
            if (debug) Console.WriteLine($"{i1} * {i2} = {result}");
            return result;
        }
    }

    private static (int nextIndex, string data) GetFirstData(string data)
    {
        var firstDontIndex = data.IndexOf(DontConstant, StringComparison.InvariantCultureIgnoreCase);
        return firstDontIndex == -1 
            ? (-1,string.Empty) 
            : (firstDontIndex,data.Substring(0, firstDontIndex + DontConstant.Length));
       
    } 

    private static (int nextIndex, string data) GetLastData(string data, int from)
    {
        var lastDoIndex = data.IndexOf(DoConstant, from, StringComparison.InvariantCultureIgnoreCase);
        return lastDoIndex == -1 
            ? (lastDoIndex,string.Empty) 
            : (lastDoIndex,data.Substring(lastDoIndex));
       
    }
    
    private static (int nextIndex, string data) GetMiddleData(string data, int from)
    {
        var nextDoIndex = data.IndexOf(DoConstant,from, StringComparison.InvariantCultureIgnoreCase);
        var nextDontIndex = data.IndexOf(DontConstant,nextDoIndex, StringComparison.InvariantCultureIgnoreCase);
        return nextDontIndex == -1 
            ? (from,string.Empty) 
            : (nextDontIndex,data.Substring(nextDoIndex, nextDontIndex-nextDoIndex+DontConstant.Length));
    }    
    
    [GeneratedRegex(@"(mul\(\d+,\d+\))")]
    private static partial Regex Mul();
}