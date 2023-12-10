using System.Text;
using System.Text.RegularExpressions;

namespace Y2023;

public static class Day03
{
    private static readonly char[] ValidSymbols = new[] { '!','\"','#','$','%','&','/','(',')','=','@','*',',','<','>','^','+','?','´','-',':',';','_','^','~','\'','<','>',','};
    private static readonly char ValidGear = '*';
    public static string Part1(char[,] data, bool debug = false)
    {
        
        if (debug) PrintArray(data);
        var es = new EngineSchematic(data);
        var result = es.PartNumbers.Sum(item => item.Value);
        
        Console.WriteLine($"Result: {result}");
        return result.ToString();
    }
    
    public static string Part2(char[,] data, bool debug = false)
    {
        if (debug) PrintArray(data);
        var es = new EngineSchematic(data);
        var result = es.Gears.Sum(gear => gear.Ratio);
        
        Console.WriteLine($"Result: {result}");
        return result.ToString();
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
        public IEnumerable<Gear> Gears;
        public int MaxCol;
        public int MaxRow;

        public EngineSchematic(char[,]data)
        {
            _data = data;
            MaxCol = data.GetLength(1) - 1;
            MaxRow = data.GetLength(0) - 1;
            Numbers = GetAllNumbers(data);
            PartNumbers = GetAllPartNumbers();
            Gears = GetAllGears();
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
        

        private IEnumerable<Number> GetAllPartNumbers()
        {
            var list = new List<Number>();
            foreach (var number in Numbers)
            {
                Console.WriteLine($"Checking {number}");
                // Define Search boundaries
                var startAdjacentRow = number.StartPosition.Row == 0  ? 0  : number.StartPosition.Row - 1;
                var startAdjacentCol = number.StartPosition.Col == 0  ? 0  : number.StartPosition.Col - 1;
                var endAdjacentRow = number.EndPosition.Row == MaxRow ? MaxRow : number.EndPosition.Row + 1;
                var endAdjacentCol = number.EndPosition.Col == MaxCol ? MaxCol : number.EndPosition.Col + 1;

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

        
        private IEnumerable<Gear> GetAllGears()
        {
            var list = new List<Gear>();
            foreach (var number in Numbers)
            {
                Console.WriteLine($"Checking {number}");
                // Define Search boundaries
                var startAdjacentRow = number.StartPosition.Row == 0  ? 0  : number.StartPosition.Row - 1;
                var startAdjacentCol = number.StartPosition.Col == 0  ? 0  : number.StartPosition.Col - 1;
                var endAdjacentRow = number.EndPosition.Row == MaxRow ? MaxRow : number.EndPosition.Row + 1;
                var endAdjacentCol = number.EndPosition.Col == MaxCol ? MaxCol : number.EndPosition.Col + 1;

                for (int row = startAdjacentRow; row <= endAdjacentRow; row++)
                {
                    var foundGear = false;
                    for (int col = startAdjacentCol; col <= endAdjacentCol; col++)
                    {
                        //Console.WriteLine($"Checking row:{row} col:{col} char: {_data[row, col]}");
                        foundGear = IsGear(_data[row, col]);
                        if (foundGear)
                        {
                            var id = $"{row}{col}";
                            Console.WriteLine($"Found gear with id {id}");
                            var gear = list.FirstOrDefault(gear => gear.Id == id) ?? new Gear(new Cell(row,col));
                            
                            if (!list.Contains(gear))
                            {
                                Console.WriteLine($"Add new gear with id {gear.Id}");
                                list.Add(gear);
                            }
                            gear.AddPart(number);
                            break;
                        }
                    }
                    if (foundGear) break;
                }
            }
            return list;
        }
        
        
        private static bool IsValidSymbol(char c)
        {
            return ValidSymbols.Contains(c);
        }
        
        
        private static bool IsGear(char c)
        {
            return c == ValidGear;
        }
    }

    
    internal class Gear
    {
        public string Id;
        private Number? _part1;
        private Number? _part2;
        public override string ToString()
        {
            return Ratio.ToString();
        }
        public Gear(Cell cell)
        {
            Id = cell.Id;
        }
        
        
        public int Ratio
        {
            get
            {
                if (_part1 == null || _part2 == null) return 0;
                return _part1.Value * _part2.Value;
            }
        }
        
        
        public void AddPart(Number part)
        {
            if (_part1 == null)
            {
                _part1 = part;
                Console.WriteLine($"Part1: Add part {part.Value} to gear {Id}");
                return;
            }
            if (_part2 == null)
            {
                _part2 = part;
                Console.WriteLine($"Part2: Add part {part.Value} to gear {Id}");
                return;
            }
            if (_part2 != null) throw new Exception($"Too many parts for gear {Id}");
        }
    }
    
    
    internal class Number
    {
        public readonly int Value;
        public readonly Cell StartPosition;
        public readonly Cell EndPosition;

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
        public string Id => $"{Row}{Col}";

        public Cell(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}