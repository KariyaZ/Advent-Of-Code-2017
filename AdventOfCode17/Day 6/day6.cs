using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    class day6
    {
        public day6()
        {
            string input = File.ReadAllText(@"day6input.txt");
            input = input.TrimEnd('\n');

            List<string> previous = new List<string>();
            previous.Add(input);
            List<int> list = input.Split('\t').Select(Int32.Parse).ToList();

            //int steps = day6_solve1(list, previous);
            //Console.WriteLine(steps);
            int loops = day6_solve2(list, previous);
            Console.WriteLine(loops);
        }

        public int day6_solve1(List<int> list, List<string> previous)
        {
            string currentstate = "";
            int steps = 0;
            for (; ;)
            {               
                int value = list.Max();
                int maxIndex = list.IndexOf(value);
                list[maxIndex] = 0;

                for (int i = 0; i < value; i++)
                {
                    maxIndex += 1;
                    if (maxIndex >= list.Count())
                    {
                        maxIndex = 0;
                    }
                    list[maxIndex] += 1;
                }
                steps += 1;

                currentstate = string.Join(",", list);

                if (previous.Contains(currentstate)) return steps;

                previous.Add(currentstate);
               
            }
        }

        public int day6_solve2(List<int> list, List<string> previous)
        {
            string currentstate = "";
            string startofLoop = "";
            bool startFound = false;
            int steps = 0;
            for (; ; )
            {
                int value = list.Max();
                int maxIndex = list.IndexOf(value);
                list[maxIndex] = 0;

                for (int i = 0; i < value; i++)
                {
                    maxIndex += 1;
                    if (maxIndex >= list.Count())
                    {
                        maxIndex = 0;
                    }
                    list[maxIndex] += 1;
                }
                steps += 1;

                currentstate = string.Join(",", list);

                if (currentstate == startofLoop) return steps;

                if (!startFound && previous.Contains(currentstate))
                {
                    startofLoop = currentstate;
                    startFound = true;
                    steps = 0;
                }

                previous.Add(currentstate);

            }
        }

    }
}
