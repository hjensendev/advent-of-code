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

        var cardCollection = GetCards(lines);
        var result = cardCollection.Cards.Sum(card => card.Value);
        
        Console.WriteLine($"Result: {result}");
        return result.ToString(CultureInfo.InvariantCulture);
    }
    
    public static string Part2(string[] lines, bool debug = false)
    {
        if (lines == null) throw new ArgumentNullException(nameof(lines));

        var cardCollection = GetCards(lines);
        cardCollection.ProcessWinningCards();

        var result = cardCollection.TotalNumberOfCards;
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
            Console.WriteLine($"Process {orgLine}");
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
        public int StartingNumberOfCards => _originalCards.Count;
        public int TotalNumberOfCards => Cards.Count();
        private bool _processedWinningCards = false;
        private readonly List<Card> _originalCards;

        public CardCollection(IEnumerable<Card> cards)
        {
            var cardList = cards.ToList();
            Cards = cardList;
            _originalCards = cardList;
        }

        public List<Card> GetOriginalCards(Range range)
        {
            return _originalCards.Take(range).ToList();
        }

        public void ProcessWinningCards()
        {
            Console.WriteLine($"Starting with {StartingNumberOfCards} cards");
            var indexOfCardToProcess = 0;
            while (!_processedWinningCards)
            {
                Console.WriteLine("--");
                var arrayOfCards = Cards.ToArray();
                var card = arrayOfCards[indexOfCardToProcess];
                Console.WriteLine($"Processing card {indexOfCardToProcess} with id {card.Id}");
                
                var rangeBeforeNewCards = new Range(0, indexOfCardToProcess);
                var rangeCurrentCard = new Range(indexOfCardToProcess,indexOfCardToProcess + 1);
                var rangeAfterNewCards = new Range(indexOfCardToProcess + 1,  arrayOfCards.Length);
                var rangeNewCards = new Range(card.Id ,card.Id + card.MyWinningNumbers.Length);
                Console.WriteLine($"Card has {card.MyWinningNumbers.Count()} winning numbers");

                var topCards = Cards.Take(rangeBeforeNewCards).ToList();
                var thisCard = Cards.Take(rangeCurrentCard).ToList();
                var bottomCards = Cards.Take(rangeAfterNewCards).ToList();
                var newCards = GetOriginalCards(rangeNewCards);
                Console.WriteLine($"Adding {newCards.Count} cards");
                
                var newListOfCards = topCards.ToList();
                newListOfCards.AddRange(thisCard);
                newListOfCards.AddRange(newCards);
                newListOfCards.AddRange(bottomCards);
                Cards = newListOfCards;
                Console.WriteLine($"CardCollection now has {TotalNumberOfCards} cards");

                indexOfCardToProcess++;
                Console.WriteLine($"Processed {indexOfCardToProcess} of {TotalNumberOfCards}");
                _processedWinningCards = indexOfCardToProcess == newListOfCards.Count;
            }
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