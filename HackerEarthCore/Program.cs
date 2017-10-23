using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HackerEarthCore
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<string> input = new List<string>();
            if (args.Any())
            {
                if (File.Exists(args[0]))
                {
                    input.AddRange(File.ReadAllLines(args[0]));
                }
                else
                {
                    input.AddRange(args.ToList().ConvertAll(arg => arg.Replace("|", " ")));
                }
            }
            else
            {
                var line = Console.ReadLine().Trim();
                while (!string.IsNullOrEmpty(line))
                {
                    input.Add(line);
                    line = Console.ReadLine().Trim();
                }
            }
            Console.WriteLine(input.Aggregate(new StringBuilder(), (a, b) => a.AppendLine(b)));
            CrossesNaughts(input);
            Console.ReadLine();
        }

        public static void WriteBoard(int dims, string[,] board)
        {
            string theBoard = "";
            for (var x = 0; x < dims; x++)
            {
                for (var y = 0; y < dims; y++)
                    theBoard += string.Format("{0} ", board[x, y]);
                theBoard += "\r\n";
            }
            Console.Write(theBoard);
        }

        public static bool TestBoard(int dims, string[,] board, bool winner, ref string test)
        {
            test = "";
            for (var x = 0; x < dims; x++)
            {
                if (winner) break;
                test = board[x, 0];
                for (var y = 0; y < dims; y++)
                {
                    if (test != board[x, y]) test = "";
                }
                if (test.Trim() == "")
                {
                    test = board[0, x];
                    for (var y = 0; y < dims; y++)
                    {
                        if (test != board[y, x]) test = "";
                    }
                }
                winner = (test.Trim() != "");
            }
            if (!winner) // check the diagonals
            {
                test = board[0, 0]; // check left top to bottom right
                for (var x = 0; x < dims; x++)
                    if (test != board[x, x]) test = "";
                if (test.Trim() == "")
                {
                    test = board[0, dims - 1]; // check left bottom to top right
                    for (var x = 0; x < dims; x++)
                    {
                        var y = dims - x - 1;
                        if (test != board[x, y]) test = "";
                    }
                }
                winner = (test.Trim() != "");
            }
            return winner;
        }

        public static void CrossesNaughts(List<string> input)
        {
            var games = int.Parse(input.First());
            input.RemoveAt(0);
            for (var game = 0; game < games; game++)
            {
                var winner = false;
                var first = input.First();
                input.RemoveAt(0);
                var dims = Int32.Parse(input.First());
                input.RemoveAt(0);
                var moves = dims * dims;
                string[,] board = new string[dims, dims];
                // Set up the board
                for (var x = 0; x < dims; x++)
                {
                    for (var y = 0; y < dims; y++)
                        board[x, y] = " ";
                }
                var i = 0;
                var test = "";
                var player = first.ToLower() == "alice" ? "A" : "B";
                while (!winner && i < moves)
                {
                    var move = input.First().Split();
                    input.RemoveAt(0);
                    var xmove = int.Parse(move[0]) - 1;
                    var ymove = int.Parse(move[1]) - 1;
                    board[xmove, ymove] = player;
                    player = player == "A" ? "B" : "A";
                    // Do we have a winner?
                    winner = TestBoard(dims, board, winner, ref test);
                    i++;
                }
                while (winner && moves > i)
                {
                    moves--;
                    input.RemoveAt(0);
                }
                Console.WriteLine(winner
                    ? string.Format("{0} wins on move {1}", test == "A" ? "Alice" : "Bob", i)
                    : "Match drawn!");
                WriteBoard(dims, board);
            }
        }

        public static void fizzbuzz()
        {
            var tests = Console.ReadLine().Trim();
            int itests;
            if (int.TryParse(tests, out itests))
            {
                var inputs = Console.ReadLine().Trim().Split(' ');
                if (inputs.Count() == itests)
                {
                    inputs.ToList()
                        .ForEach(input =>
                        {
                            int end;
                            if (int.TryParse(input, out end))
                            {
                                for (var i = 1; i <= end; i++)
                                {
                                    if (i % 3 == 0)
                                        Console.Write("Fizz");
                                    if (i % 5 == 0)
                                        Console.Write("Buzz");
                                    if (i % 3 != 0 && i % 5 != 0)
                                        Console.Write(i);
                                    Console.WriteLine();
                                }
                            }
                        });
                }
            }
        }

    }
}
