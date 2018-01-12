using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    class day21
    {
        public day21() 
        {
            string[] input = File.ReadAllLines(@"day21input.txt");
            var separators = new char[] { ' ', '=', '>' };
            Dictionary<string, string> rules = new Dictionary<string, string>();

            foreach (var rule in input)
            {
                var tokens = rule.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                string from = tokens[0];
                string to = tokens[1];

                if (!rules.ContainsKey(from)) rules.Add(from, to);
                if (!rules.ContainsKey(FlipHorizontal(from))) rules.Add(FlipHorizontal(from), to);
                if (!rules.ContainsKey(FlipVertical(from))) rules.Add(FlipVertical(from), to);

                var newFrom = Rotate(from);
                if (!rules.ContainsKey(newFrom)) rules.Add(newFrom, to);
                if (!rules.ContainsKey(FlipHorizontal(newFrom))) rules.Add(FlipHorizontal(newFrom), to);
                if (!rules.ContainsKey(FlipVertical(newFrom))) rules.Add(FlipVertical(newFrom), to);

            }

            string[] grid = new string[]
            {
            ".#.",
            "..#",
            "###",
            };

            for (int i = 0; i < 18; i++)
            {
                if (grid.Length % 2 == 0)
                {
                    grid = Increase(grid, rules, 2);
                }
                else
                {
                    grid = Increase(grid, rules, 3);
                }
            }

            int Hashcount = 0;

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid.Length; j++)
                {
                    if (grid[i][j] == '#')
                    {
                        Hashcount++;
                    }
                }
            }

            Console.WriteLine(Hashcount);
        }

        public static string FlipHorizontal(string grid)
        {
            string[] rows = grid.Split('/');
            string[] newRow = new string[rows.Length];

            for (int i = 0; i < rows.Length; i++)
            {
                newRow[i] = string.Join("", rows[i].Reverse());
            }

            return string.Join("/", newRow);
        }

        public static string FlipVertical(string grid)
        {
            string[] rows = grid.Split('/');
            string[] newRow = new string[rows.Length];

            for (int i = 0; i < rows.Length; i++)
            {
                newRow[rows.Length - i - 1] = rows[i];
            }

            return string.Join("/", newRow);
        }

        public static string Rotate(string grid)
        {
            string[] rows = grid.Split('/');
            char[,] newRows = new char[rows.Length, rows.Length];

            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < rows.Length; j++)
                {
                    newRows[rows.Length - j - 1, i] = rows[i][j];
                }
            }

            string[] sNewRows = new string[rows.Length];
            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < rows.Length; j++)
                {
                    sNewRows[i] += newRows[i, j];
                }
            }

            string result = string.Join("/", sNewRows);
            return result;
        }

        public static string CopyFrom(string[] grid, int startRow, int startColumn, int size)
        {
            string[] section = new string[size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    section[i] += grid[i + startRow][j + startColumn];
                }
            }

            return string.Join("/", section);
        }

        public static void CopyTo(string[] grid, string section, int startRow, int size)
        {
            string[] rows = section.Split('/');
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[startRow + i] += rows[i][j];
                }
            }
        }

        public static string[] Increase(string[] grid, Dictionary<string, string> rules, int size)
        {
            int newSize = size + 1;
            string[] newGrid = new string[grid.Length / size * newSize];

            for (int i = 0; i * size < grid.Length; i++)
            {
                for (int j = 0; j * size < grid.Length; j++)
                {
                    string section = CopyFrom(grid, i * size, j * size, size);
                    CopyTo(newGrid, rules[section], i * newSize, newSize);
                }
            }

            return newGrid;
        }
    }
}
