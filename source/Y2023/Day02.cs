using System.Text.RegularExpressions;
namespace Y2023;

public static class Day02
{
    private const string PatternGame = @"Game \d+:";
    private const string PatternNumberOfRed = @"\d+ red";
    private const string PatternNumberOfGreen = @"\d+ green";
    private const string PatternNumberOfBlue= @"\d+ blue";
    private const string Count= @"\d+";
    
    
    
    public static string Part1(string[] lines, bool debug = false)
    {
        if (lines == null) throw new ArgumentNullException(nameof(lines));

        var games = GetGames(lines);

        var validGameSum = 0;
        var validGameCount = 0;
        foreach (var game in games)
        {
            if (!Bag1.IsValidGame(game)) continue;
            validGameSum += game.Number;
            validGameCount++;
        }
        Console.WriteLine($"{validGameCount} of {games.Count} are valid");
        Console.WriteLine($"The sum of valid games are {validGameSum}");
        return validGameSum.ToString();
    }
    
    
    
    
    
    public static string Part2(string[] lines, bool debug = false)
    {
        if (lines == null) throw new ArgumentNullException(nameof(lines));
        var games = GetGames(lines);
        var sumOfPowers = 0;
        foreach (var game in games)
        {
            if (debug) Console.WriteLine($"Game {game.Number}: Power:{game.PowerOfMinCubes} mR: {game.MinRed} mG: {game.MinGreen} mB: {game.MinBlue}");
            sumOfPowers += game.PowerOfMinCubes;
        }
        return sumOfPowers.ToString();
    }


    private static List<Game> GetGames(IEnumerable<string> lines)
    {
        var countPattern = new Regex(Count);
        var gamePattern = new Regex(PatternGame);
        var numberOfRedPattern = new Regex(PatternNumberOfRed);
        var numberOfGreenPattern = new Regex(PatternNumberOfGreen);
        var numberOfBluePattern = new Regex(PatternNumberOfBlue);
        var games = new List<Game>();

        foreach (var line in lines)
        {
            var draws = new List<Draw>();
            var gameLine = gamePattern.Match(line);
            var gameNumber = Convert.ToInt32(countPattern.Match(gameLine.Value).Value);

            var drawsLine = line[(line.IndexOf(':')+1)..].Split(';');
            foreach (var drawLine in drawsLine)
            {
                var red = numberOfRedPattern.Match(drawLine);
                var green = numberOfGreenPattern.Match(drawLine);
                var blue = numberOfBluePattern.Match(drawLine);
                var r = red.Length > 0 ?  Convert.ToInt32(red.Value.Replace(" red", "")) : 0;
                var g = green.Length > 0 ? Convert.ToInt32(green.Value.Replace(" green", "")) : 0;
                var b = blue.Length > 0 ? Convert.ToInt32(blue.Value.Replace(" blue", "")) : 0;
                draws.Add(new Draw(r,g,b));
            }
            games.Add(new Game(gameNumber, draws));
        }
        return games;
    }
    
    private static class Bag1
    {
        private static int _red = 12;
        private static int _green = 13;
        private static int _blue = 14;

        public static bool IsValidGame(Game game)
        {
            foreach (var draw in game.Draws)
            {
                var invalidRed = (draw.Red > _red);
                var invalidGreen = (draw.Green > _green);
                var invalidBlue = (draw.Blue > _blue);
                if (invalidRed || invalidGreen || invalidBlue)
                {
                    Console.WriteLine($"Game {game.Number} is invalid.");
                    if (invalidRed) Console.WriteLine($"Too many red: {draw.Red} > {_red}");
                    if (invalidGreen) Console.WriteLine($"Too many green: {draw.Green} > {_green}");
                    if (invalidBlue) Console.WriteLine($"Too many blue: {draw.Blue} > {_blue}");
                    return false;
                }
            }
            return true;
        }
    }
    
    private class Game
    {
        public readonly int Number;
        public readonly List<Draw> Draws;
        public int MinRed;
        public int MinGreen;
        public int MinBlue;
        public int PowerOfMinCubes;
        public Game(int number, List<Draw>draws)
        {
            Number = number;
            Draws = draws;

            MinRed = draws.Max(draw => draw.Red);
            MinGreen = draws.Max(draw => draw.Green);
            MinBlue = draws.Max(draw => draw.Blue);
            PowerOfMinCubes = MinRed * MinGreen * MinBlue;
        }
    }
    
    private class Draw
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

        public Draw(int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }
    }
}


