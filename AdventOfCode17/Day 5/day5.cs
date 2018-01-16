using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    class day5
    {
        public day5()
        {
            string input = File.ReadAllText(@"day5input.txt");
            input = input.TrimEnd('\n');
            int steps = day5_solve1(input);
            int steps2 = day5_solve2(input);

            Console.WriteLine(steps + "\n" + steps2);
        }
        public int day5_solve1(string input)
        {
            List<int> list = input.Split('\n').Select(Int32.Parse).ToList();
            int index = 0;
            int steps = 0;
            
            while (index > -1 && index < list.Count())
            {
                int value = list[index];
                list[index] = value + 1;
                index += value;
                steps += 1;
            } 
      
            return steps;
        }

        public int day5_solve2(string input)
        {
            List<int> list = input.Split('\n').Select(Int32.Parse).ToList();
            int index = 0;
            int steps = 0;

            while (index > -1 && index < list.Count())
            {
                int value = list[index];
                if (value >= 3)
                {
                    list[index] = value - 1;
                } else
                {
                    list[index] = value + 1;
                }  
                index += value;
                steps += 1;
            }

            return steps;
        }
    }
}
