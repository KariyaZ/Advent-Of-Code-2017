using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    class StartUp
    {
        static void Main()
        {
            string input = File.ReadAllText(@"day2input.txt");
            new day2(input);
            Console.ReadKey();
        }
    }
}
