using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace Y2023;

public static class Day03
{
    public static char[] ValidSymbols = new[] { '!','\"','#','$','%','&','/','(',')','=','@','*',',','<','>','^','+','?','´','-',':',';','_','^','~','\'','<','>',','};
    public static string Part1(char[,] data, bool debug = false)
    {
        
        var result = 0;
        if (debug) PrintArray(data);
        var es = new EngineSchematic(data);
        result = es.PartNumbers.Sum(item => item.Value);
        
        Console.WriteLine($"Result: {result}");
        return result.ToString();
    }

    private static IEnumerable<Number> GetAllNumbers(char[,] data)
    {
        var filterNumber= @"\d+";
        var patternNumber = new Regex(filterNumber);
        var numbers = new List<Number>();

        for (var row = 0; row < data.GetLength(0); row++)
        {
            var line = new StringBuilder();
            for (var col = 0; col < data.GetLength(1); col++)
            {
                line.Append(data[row,col]);
            }

            var match = patternNumber.Match(line.ToString());
            while (match.Success)
            {
                numbers.Add(new Number(Convert.ToInt32(match.Value),new Cell(row,match.Index),new Cell(row,match.Index+ match.Length - 1)));
                match = match.NextMatch();
            }
        }
        return numbers;
    }

    private static void PrintArray(char[,] data)
    {
        for (int row = 0; row < data.GetLength(0); row++)
        {
            Console.Write($"{row}:");
            for (var col = 0; col < data.GetLength(1); col++)
            {
                Console.Write(data[row, col]);
            }  
            Console.Write("\r\n");
        }    
    }
    

    internal class EngineSchematic
    {
        private char[,] _data;
        public IEnumerable<Number> Numbers;
        public IEnumerable<Number> PartNumbers;
        public int MaxCol;
        public int MaxRow;

        public EngineSchematic(char[,]data)
        {
            _data = data;
            MaxCol = data.GetLength(1) - 1;
            MaxRow = data.GetLength(0) - 1;
            Numbers = GetAllNumbers(data);
            PartNumbers = GetAllPartNumbers();
        }

        private IEnumerable<Number> GetAllPartNumbers()
        {
            var list = new List<Number>();
            int startAdjacentRow = 0;
            int startAdjacentCol = 0;
            int endAdjacentRow = 0;
            int endAdjacentCol = 0;

            foreach (var number in Numbers)
            {
                Console.WriteLine($"Checking {number}");
                // Define Search boundaries
                startAdjacentRow = number.StartPosition.Row == 0  ? 0  : number.StartPosition.Row - 1;
                startAdjacentCol = number.StartPosition.Col == 0  ? 0  : number.StartPosition.Col - 1;
                endAdjacentRow = number.EndPosition.Row == MaxRow ? MaxRow : number.EndPosition.Row + 1;
                endAdjacentCol = number.EndPosition.Col == MaxCol ? MaxCol : number.EndPosition.Col + 1;

                for (int row = startAdjacentRow; row <= endAdjacentRow; row++)
                {
                    var foundSymbol = false;
                    for (int col = startAdjacentCol; col <= endAdjacentCol; col++)
                    {
                        Console.WriteLine($"Checking row:{row} col:{col} char: {_data[row, col]}");
                        foundSymbol = IsValidSymbol(_data[row, col]);
                        if (foundSymbol)
                        {
                            Console.WriteLine("found symbol");
                            list.Add(number);
                            break;
                        }
                    }
                    if (foundSymbol) break;
                }
            }
            return list;
        }

        private bool IsValidSymbol(char c)
        {
            return ValidSymbols.Contains(c);
        }
    }
    
    
    internal class Number
    {
        public int Value;
        public Cell StartPosition;
        public Cell EndPosition;
        public override string ToString()
        {
            return $"{Value}:{StartPosition}:{EndPosition}";
        }

        public Number(int value, Cell startPosition, Cell endPosition)
        {
            Value = value;
            StartPosition = startPosition;
            EndPosition = endPosition;
        }
    }
    

    internal class Cell
    {
        public int Row;
        public int Col;
        public override string ToString()
        {
            return $"{Row},{Col}";
        }

        public Cell(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}