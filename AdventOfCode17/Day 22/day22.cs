using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    class day22
    {
        public day22()
        {
            day22_solve2();
        }

        private void day22_solve1()
        {
            var input = File.ReadAllLines(@"day22input.txt");
            List<List<char>> grid = new List<List<char>>();
            foreach (string line in input)
            {
                grid.Add(line.ToList());
            }

            Tuple<int, int> carrierpos = new Tuple<int, int>((grid.Count() - 1) / 2, (grid[0].Count - 1) / 2);
            string direction = "up";
            int bursts = 0;

            for (int i = 0; i < 10000; i++)
            {
                if (carrierpos.Item1 >= grid.Count() || carrierpos.Item2 >= grid.Count() || carrierpos.Item1 < 0 || carrierpos.Item2 < 0)
                {
                    grid = inc_Size(grid);
                    carrierpos = new Tuple<int, int>(carrierpos.Item1 + 1, carrierpos.Item2 + 1);
                }
                char value = grid[carrierpos.Item1][carrierpos.Item2];
                direction = changedir(direction, value);
                if (value == '#')
                {
                    grid[carrierpos.Item1][carrierpos.Item2] = '.';
                }
                else
                {
                    grid[carrierpos.Item1][carrierpos.Item2] = '#';
                    bursts++;
                }
                carrierpos = newPos(carrierpos, direction);
            }
            Console.WriteLine(bursts);
        }

        public void day22_solve2()
        {
            var input = File.ReadAllLines(@"day22input.txt");
            List<List<char>> grid = new List<List<char>>();
            foreach (string line in input)
            {
                grid.Add(line.ToList());
            }

            Tuple<int, int> carrierpos = new Tuple<int, int>((grid.Count() - 1) / 2, (grid[0].Count - 1) / 2);
            string direction = "up";
            int bursts = 0;

            for (int i = 0; i < 10000000; i++)
            {
                if (carrierpos.Item1 >= grid.Count() || carrierpos.Item2 >= grid.Count() || carrierpos.Item1 < 0 || carrierpos.Item2 < 0)
                {
                    grid = inc_Size(grid);
                    carrierpos = new Tuple<int, int>(carrierpos.Item1 + 1, carrierpos.Item2 + 1);
                }
                char value = grid[carrierpos.Item1][carrierpos.Item2];
                direction = changedir(direction, value);
                if (value == '#')
                {
                    grid[carrierpos.Item1][carrierpos.Item2] = 'F';
                }
                if (value == '.')
                {
                    grid[carrierpos.Item1][carrierpos.Item2] = 'W';
                }
                if (value == 'F')
                {
                    grid[carrierpos.Item1][carrierpos.Item2] = '.';
                }
                if (value == 'W')
                {
                    grid[carrierpos.Item1][carrierpos.Item2] = '#';
                    bursts++;
                }
                carrierpos = newPos(carrierpos, direction);
            }
            Console.WriteLine(bursts);
        }
        private static List<List<char>> inc_Size(List<List<char>> grid)
        {
            int width = grid[0].Count()+2;

            foreach (List<char> row in grid)
            {
                row.Add('.');
                row.Insert(0, '.');
            }

            List<char> newRow = new List<char>();
            List<char> newRow2 = new List<char>();

            for (int i = 0; i < width; i++)
            {
                newRow.Add('.');
                newRow2.Add('.');
            }

            grid.Add(newRow);
            grid.Insert(0,newRow2);
            
            return grid;
        }

        private static string changedir(string currentdir, char value)
        {
            string newdirection = "";

            if (value == '#')
            {
                if (currentdir == "up") newdirection = "right";
                if (currentdir == "right") newdirection = "down";
                if (currentdir == "down") newdirection = "left";
                if (currentdir == "left") newdirection = "up";
            }
            if (value == '.')
            {
                if (currentdir == "up") newdirection = "left";
                if (currentdir == "right") newdirection = "up";
                if (currentdir == "down") newdirection = "right";
                if (currentdir == "left") newdirection = "down";
            }
            if (value == 'F')
            {
                if (currentdir == "up") newdirection = "down";
                if (currentdir == "right") newdirection = "left";
                if (currentdir == "down") newdirection = "up";
                if (currentdir == "left") newdirection = "right";
            }
            if (value == 'W')
            {
                newdirection = currentdir;
            }
            return newdirection;
        }

        private static Tuple<int,int> newPos(Tuple<int,int> position, string dir)
        {
            switch (dir)
            {
                case "up":
                    return new Tuple<int, int>(position.Item1 - 1, position.Item2);
                case "down":
                    return new Tuple<int, int>(position.Item1 + 1, position.Item2);
                case "left":
                    return new Tuple<int, int>(position.Item1, position.Item2 - 1);
                case "right":
                    return new Tuple<int, int>(position.Item1, position.Item2 + 1);
                default:
                    throw new Exception();
            }
        }
    }
}
