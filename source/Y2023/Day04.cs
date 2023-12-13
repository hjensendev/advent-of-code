using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Y2023;

public static class Day04
{
    private const int MaxThreads = 8;
    private const string PatternGame = @"\d+:";
    private const string PatternCount= @"\d+";
    private const char WinNumbersSeparator = ':';
    private const char MyNumbersSeparator = '|';
    private static bool _debug;
    
    public static string Part1(string[] lines, bool debug = false)
    {
        if (lines == null) throw new ArgumentNullException(nameof(lines));
        _debug = debug;

        var cardCollection = GetCards(lines);
        var result = cardCollection.Cards.Sum(card => card.Value);
        
        Console.WriteLine($"Result: {result}");
        return result.ToString(CultureInfo.InvariantCulture);
    }
    
    public static string Part2(string[] lines, bool debug = false)
    {
        if (lines == null) throw new ArgumentNullException(nameof(lines));
        _debug = debug;
        
        var cardCollection = GetCards(lines);
        cardCollection.ProcessWinningCards();

        var result = cardCollection.Cards.Count();
        Console.WriteLine($"Result: {result}");
        return result.ToString(CultureInfo.InvariantCulture);
    }

    private static CardCollection GetCards(IEnumerable<string> lines)
    {
        var countPattern = new Regex(PatternCount);
        var gamePattern = new Regex(PatternGame);

        var cards = new List<Card>();
        foreach (var orgLine in lines)
        {
            var line =  orgLine.Replace("  ", " ");
            if (_debug)  Console.WriteLine($"Process {orgLine}");
            var gameLine = gamePattern.Match(line);
            var gameNumber = Convert.ToInt32(countPattern.Match(gameLine.Value).Value);
            var winningNumbers = line.Substring(line.IndexOf(WinNumbersSeparator) + 2 ,
                line.IndexOf(MyNumbersSeparator) - line.IndexOf(WinNumbersSeparator)-3);
            var myNumbers = line.Substring(line.IndexOf(MyNumbersSeparator) + 2);
            cards.Add(new Card(gameNumber, winningNumbers.Split(' '), myNumbers.Split(' ')));
        }
        return new CardCollection(cards);
    }

    private class CardCollection
    {
        public IEnumerable<Card> Cards;
        private readonly IEnumerable<Card> _originalCards;

        public CardCollection(IEnumerable<Card> cards)
        {
            Cards = cards;
            _originalCards = Cards.ToArray();
        }
        

        public void ProcessWinningCards()
        {
            Console.WriteLine($"Starting with {_originalCards.Count()} cards");

            var sw = new Stopwatch();
            sw.Start();
            var arrayOfCards = Cards.ToArray();

            var range = ProcessRangeOfCards(0, arrayOfCards.Length);
            Cards = range;
 
            sw.Stop();
            Console.WriteLine($"Processed {Cards.Count()} cards in {sw.Elapsed}");
        }

        private IEnumerable<Card> ProcessRangeOfCards(int start, int end)
        {
            var cardsToProcess = Cards.Take(new Range(start, end)).ToList();
            var swCycle = new Stopwatch();
            swCycle.Start();
            
            var i = start;
            while (i <= cardsToProcess.Count - 1)
            {
                if (_debug) Console.WriteLine($"Process card at index {i}");
                var card = cardsToProcess.ElementAt(i);
                
                if (_debug) Console.WriteLine($"Card id:{card.Id} has {card.MyWinningNumbers.Length} winners");
                var prizeCards = ProcessCard(card);
                cardsToProcess.AddRange(prizeCards);
                i++;
            }
            swCycle.Stop();
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} completed cycle in {swCycle.Elapsed}");
            return cardsToProcess;
        }
        
        
        private IEnumerable<Card> ProcessCard(Card card)
        {
            var result = new List<Card>();
            for (var j = 0; j < card.MyWinningNumbers.Length; j++)
            {
                var prizeCard = _originalCards.ElementAt(card.Id + j);
                if (_debug) Console.WriteLine($"Add card with id {prizeCard.Id} as prize");
                result.Add(prizeCard);
            }
            return result;
        }
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
            if (_debug) Console.WriteLine($"Card {Id} has {MyWinningNumbers.Count()} winners and is worth {Value}");
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