using System;
using System.IO;
using System.Linq;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace AdventOfCode17
{
    public class day2
    {
        public day2()
        {
            string input = File.ReadAllText(@"day2input.txt");
            string trimmed = input.TrimEnd('\n');
            int checksum1 = day2_solve1(trimmed);
            Console.WriteLine(checksum1);
            int checksum2 = day2_solve2(trimmed);
            Console.WriteLine(checksum2);
        }

        public int day2_solve1(string input)
        {
            string[] splitLines = input.Split('\n');
            int checksum = 0;
            foreach (string line in splitLines)
            {
                int[] numbers = Array.ConvertAll(line.Split('\t'), int.Parse);
                checksum += numbers.Max() - numbers.Min();
            }
            return checksum;
        }

        public int day2_solve2(string input)
        {
            string[] splitLines = input.Split('\n');
            int checksum = 0;
            foreach (string line in splitLines)
            {
                int[] numbers = Array.ConvertAll(line.Split('\t'), int.Parse);
                foreach (int x in numbers)
                {
                    foreach (int y in numbers)
                    {
                        if (x == y) continue;
                        if (x % y == 0)
                        {
                            checksum += x / y;
                        }
                    }
                }
            }
            return checksum;
        }
    }
}