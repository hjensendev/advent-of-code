using System.Globalization;
using System.Text.RegularExpressions;

namespace Y2023;

public static class Day04
{
    private const string PatternGame = @"\d+:";
    private const string PatternCount= @"\d+";
    private const char WinNumbersSeparator = ':';
    private const char MyNumbersSeparator = '|';
    
    public static string Part1(string[] lines, bool debug = false)
    {
        if (lines == null) throw new ArgumentNullException(nameof(lines));

        var cards = GetCards(lines);
        var result = cards.Sum(card => card.Value);
        
        Console.WriteLine($"Result: {result}");
        return result.ToString(CultureInfo.InvariantCulture);
    }

    private static IEnumerable<Card> GetCards(IEnumerable<string> lines)
    {
        var countPattern = new Regex(PatternCount);
        var gamePattern = new Regex(PatternGame);
        
        var cards = new List<Card>();
        foreach (var orgLine in lines)
        {
            var line =  orgLine.Replace("  ", " ");
            Console.WriteLine($"Process {orgLine}");
            var gameLine = gamePattern.Match(line);
            var gameNumber = Convert.ToInt32(countPattern.Match(gameLine.Value).Value);
            var winningNumbers = line.Substring(line.IndexOf(WinNumbersSeparator) + 2 ,
                line.IndexOf(MyNumbersSeparator) - line.IndexOf(WinNumbersSeparator)-3);
            var myNumbers = line.Substring(line.IndexOf(MyNumbersSeparator) + 2);
            cards.Add(new Card(gameNumber, winningNumbers.Split(' '), myNumbers.Split(' ')));
        }
        return cards;
    }

    private class Card
    {
        public readonly int Id;
        public readonly string[] WinningNumbers;
        public readonly string[] MyNumbers;
        public readonly string[] MyWinningNumbers;
        public readonly double Value;
        
        public Card(int id, string[] winningNumbers, string[] myNumbers)
        {
            Id = id;
            WinningNumbers = winningNumbers;
            MyNumbers = myNumbers;
            MyWinningNumbers = GetMyWinningNumbers();
            Value = CalculateCardValue();
            Console.WriteLine($"Card {Id} has {MyWinningNumbers.Count()} winners and is worth {Value}");
        }

        private string[] GetMyWinningNumbers()
        {
            var winners = new List<string>();
            foreach (var winNumber in WinningNumbers)
            {
                foreach (var myNumber in MyNumbers)
                {
                    if (myNumber == winNumber) winners.Add(myNumber);
                }
            }
            return winners.ToArray();
        }

        private double CalculateCardValue()
        {
            if (MyWinningNumbers.Length == 0) return 0;
            if (MyWinningNumbers.Length == 1) return 1;
            return Math.Pow(2,MyWinningNumbers.Length-1);
        }
    }
}