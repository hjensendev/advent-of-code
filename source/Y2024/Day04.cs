using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using Aoc.Core.Extensions;

namespace Y2024;

public class Day04
{
    public static string Part1(string[] input, bool debug = false)
    {
        var board = new Board(input,debug);
        return board.Count("XMAS").ToString();
    }

    private class Board
    {
        private int Width { get; }
        private int Height { get; }
        private readonly char[,] _grid;
        private readonly bool _debug;
        private int _highlightedCellRow = -1;
        private int _highlightedCellColumn = -1;

        public Board(string[] data, bool debug = false)
        {
            _debug = debug;
            Width = data.GetLength(0);
            Height = data.Length;
            _grid = new char[Height, Width ];

            for (var row = 0; row <= Height-1; row++)
            {
                for (var col = 0; col <= Width-1; col++)
                {
                    _grid[row, col] = data[row][col];
                }
            }
            Print();
        }

        private void Print()
        {
            if (!_debug) return;
            Console.Clear();
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            // Skriv ut rutenettet
            for (var row = 0; row< Height; row++)
            {
                for (var col = 0; col < Width; col++)
                {
                    Console.Write($"{_grid[row, col]}");
                }
                Console.WriteLine();
            }            
        }
        
        private void Print(int row, int col)
        {
            if (!_debug) return;
            if (_highlightedCellRow != -1 && _highlightedCellColumn != -1)
            {
                Console.SetCursorPosition(_highlightedCellColumn, _highlightedCellRow);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(_grid[_highlightedCellRow,_highlightedCellColumn]);
            }
            Console.SetCursorPosition(col, row);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(_grid[row,col]);
            _highlightedCellRow = row;
            _highlightedCellColumn = col;
            
            Thread.Sleep(1);            
        }        

        public int Count(string keyword)
        {
            var horizontalBoard = BoardLeftToRight();
            var verticalBoard = BoardTopToBottom();
            var diagonalBoardDown = BoardDiagonallyDown();
            var diagonalBoardUp = BoardDiagonallyUp();
            var totalCount = 0;
            
            totalCount += CountKeyword(keyword, horizontalBoard);
            totalCount += CountKeyword(keyword, horizontalBoard.Backwards());
            totalCount += CountKeyword(keyword, verticalBoard);
            totalCount += CountKeyword(keyword, verticalBoard.Backwards());
            totalCount += CountKeyword(keyword, diagonalBoardDown);
            totalCount += CountKeyword(keyword, diagonalBoardDown.Backwards());
            totalCount += CountKeyword(keyword, diagonalBoardUp);
            totalCount += CountKeyword(keyword, diagonalBoardUp.Backwards());
            Console.WriteLine($"Total Count: {totalCount}");
            return totalCount;
        }

        private int CountKeyword(string keyword, string board)
        {
            var matches = Regex.Matches(board, keyword);
            if (_debug) Console.WriteLine($"{matches.Count}:{board}");
            return matches.Count;
        }

        private string BoardLeftToRight()
        {
            Print();
            var sb = new StringBuilder();
            for (var row = 0; row < Height; row++)
            {
                for (var col = 0; col < Width; col++)
                {
                    Print(row,col);
                    sb.Append(_grid[row, col]);
                }
                sb.Append('.');
            }
            return sb.ToString();
        }

        private string BoardTopToBottom()
        {
            Print();            
            var sb = new StringBuilder();
            for (var col = 0; col < Width; col++)
            {
                for (var row = 0; row < Height; row++)
                {
                    Print(row,col);
                    sb.Append(_grid[row, col]);
                }
                sb.Append('.');
            }
            return sb.ToString();
        }        
        
        private string BoardDiagonallyDown()
        {
            var sb = new StringBuilder();
            
            Print();
            for (var col = 0; col < Width; col++)
            {
                var row = 0;
                var currentCol = col;
                while (currentCol >= 0 && row < Height)
                {
                    Print(row,currentCol);
                    sb.Append(_grid[row, currentCol]);
                    row++;
                    currentCol--;
                }
                sb.Append('.');
            }
            
            for (var row = 1; row < Height; row++)
            {
                var curRow = row;
                var col = Width -1;
                while (curRow < Height && col >= 0)
                {
                    Print(curRow, col);
                    sb.Append(_grid[curRow, col]);
                    curRow++;
                    col--;
                }
                sb.Append('.');
            }        
            return sb.ToString();
        }
        
        private string BoardDiagonallyUp()
        {
            var sb = new StringBuilder();

            Print();
            for (int row = Height - 1; row >= 0; row--)
            {
                int currentRow = row;
                int col = 0;
                while (currentRow < Height && col < Width)
                {
                    Print(currentRow,col);
                    sb.Append(_grid[currentRow, col]);
                    currentRow++;
                    col++;
                }
                sb.Append('.');
            }
            
            for (var col = 1; col < Width; col++)
            {
                var currentCol = col;
                var row = 0;
                while (currentCol < Width && row < Height)
                {
                    Print(row,currentCol);
                    sb.Append(_grid[row, currentCol]);
                    row++;
                    currentCol++;
                }
                sb.Append('.');
            }            
            return sb.ToString();
        }        
    }
}


