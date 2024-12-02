
using Microsoft.VisualBasic;

namespace Y2024;

public class Day02
{
    public static string Part1(string[] data, bool debug = false)
    {
        var reports = data.Select(line => new NormalReport(line)).ToList();
        var safeCount = reports.Where(r => r.IsSafe(debug:debug)).ToArray().Length;
        Console.WriteLine($"Safe levels: {safeCount}");
        return safeCount.ToString();
    }

    
    public static string Part2(string[] data, bool debug = false)
    {
        var reports = data.Select(line => new ProblemDampenerReport(line)).ToList();
        var safeCount = reports.Where(r => r.IsSafe(debug:debug)).ToArray().Length;
        Console.WriteLine($"Safe levels: {safeCount}");
        return safeCount.ToString();
    }   
    
    
    private class NormalReport
    {
        private readonly int[] _levels = Array.Empty<int>();
        public NormalReport(string data)
        {
            var numbers = data.Split(" ");
            foreach (var number in numbers)
            {
                _levels = _levels.Append(Convert.ToInt32(number)).ToArray();
            }
        }

        public bool IsSafe(bool debug = false)
        {
            return !GetNextBadLevel(_levels, debug).HasValue;
        }
    }
    
    
    private class ProblemDampenerReport
    {
        private readonly int[] _levels = Array.Empty<int>();
        public ProblemDampenerReport(string data)
        {
            var numbers = data.Split(" ");
            foreach (var number in numbers)
            {
                _levels = _levels.Append(Convert.ToInt32(number)).ToArray();
            }
        }
        
        public bool IsSafe(bool debug = false)
        {
            if (debug) Console.WriteLine($"Process {ArrayToString(_levels)}");
            var badLevel = GetNextBadLevel(_levels,debug);
            if (!badLevel.HasValue)
            {
                if (debug) Console.WriteLine($"{ArrayToString(_levels)} is safe");
                return true;
            }

            var listWithCurrentLevelRemoved = new List<int>();
            var listWithPreviousLevelRemoved = new List<int>();
            var listWithNextLevelRemoved = new List<int>();
            
            foreach (var level in _levels)
            {
                listWithCurrentLevelRemoved.Add(level);
                listWithPreviousLevelRemoved.Add(level);
                listWithNextLevelRemoved.Add(level);
            }
            
            listWithCurrentLevelRemoved.RemoveAt(badLevel.Value);
            if (badLevel.Value > 0) listWithPreviousLevelRemoved.RemoveAt(badLevel.Value - 1);
            if (badLevel.Value != _levels.Length) listWithNextLevelRemoved.RemoveAt(badLevel.Value + 1);
            
            var badLevel2 = GetNextBadLevel(listWithCurrentLevelRemoved.ToArray(), debug);
            if (!badLevel2.HasValue)
            {
                if (debug) Console.WriteLine($"{ArrayToString(_levels)} is safe after removing current level value");
                return true;
            }
            
            var badLevel3 = GetNextBadLevel(listWithPreviousLevelRemoved.ToArray(), debug);
            if (!badLevel3.HasValue)
            {
                if (debug) Console.WriteLine($"{ArrayToString(_levels)} is safe after removing previous level value");
                return true;
            }

            var badLevel4 = GetNextBadLevel(listWithNextLevelRemoved.ToArray(), debug);
            if (!badLevel4.HasValue)
            {
                if (debug) Console.WriteLine($"{ArrayToString(_levels)} is safe after removing next level value");
                return true;
            }
            
            if (debug) Console.WriteLine($"{ArrayToString(_levels)} is not safe");
            return false;
        }
    }

    private static int? GetNextBadLevel(int[] levels, bool debug = false)
    {
        if (debug) Console.WriteLine(ArrayToString(levels));
        const int minDeviation = 1;
        const int maxDeviation = 3;
        var increases = 0;
        var decreases = 0;
        
        for (var i = 0; i < levels.Length-1; i++)
        {
            if (debug) Console.WriteLine($"level{i}: {levels[i]}->{levels[i+1]}={levels[i+1] - levels[i]}"); 
                
            switch (Math.Abs(levels[i] - levels[i+1]))
            {
                case > maxDeviation:
                case < minDeviation:
                    return i;
            }

            if (levels[i] - levels[i+1] > 0) increases += 1;
            if (levels[i] - levels[i+1] < 0) decreases += 1;
            if (increases > 0 && decreases > 0 ) return i;
        }
        return null;
    }

    private static string ArrayToString(int[] data)
    {
        return string.Join(" ", data);
    }
}