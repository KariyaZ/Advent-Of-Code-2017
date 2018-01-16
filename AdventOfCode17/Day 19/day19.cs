using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    class day19
    {
        public day19()
        {

            List<char> letters = new List<char>();
            var steps = 1;
            var input = File.ReadAllText(@"day19input.txt");
            var lines = input.Split('\n').ToList();

            string direction = "down";
            var position = new Tuple<int, int>(0, lines[0].IndexOf('|'));

            while (true)
            {
                position = Next_Position(position, direction);
                steps++;
                char next = lines[position.Item1][position.Item2];
                if (next == ' ')
                {
                    steps--;
                    Console.WriteLine(string.Join(string.Empty, letters) + '\n' + steps.ToString());
                    break;
                }

                if (next == '|' || next == '-')
                {
                    continue;
                }

                if (next >= 'A' && next <= 'Z')
                {
                    letters.Add(next);
                    continue;
                }

                if (next == '+')
                {
                    direction = Next_Direction(position, direction, lines);
                    continue;
                }
            }
        }

        

        private static Tuple<int,int> Next_Position(Tuple<int , int> position, string direction)
        {
            switch (direction)
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

        private static string Next_Direction(Tuple<int, int> position, string direction, List<string> lines)
        {
            if (direction == "down" || direction == "up")
            {
                if (lines[position.Item1][position.Item2 - 1] == '-')
                {
                    return "left";
                }

                if (lines[position.Item1][position.Item2 + 1] == '-')
                {
                    return "right";
                }
            }

            if (direction == "left" || direction == "right")
            {
                if (lines[position.Item1 - 1][position.Item2] == '|')
                {
                    return "up";
                }

                if (lines[position.Item1 + 1][position.Item2] == '|')
                {
                    return "down";
                }
            }
            throw new Exception();
        }
    }
}
