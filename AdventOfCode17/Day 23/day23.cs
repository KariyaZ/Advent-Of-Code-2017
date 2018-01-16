using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    class day23
    {
        public day23()
        {
            day23_solve2();
        }
        public void day23_solve1()
        {
            var input = File.ReadAllLines(@"day23input.txt");
            Dictionary<char, long> registers = new Dictionary<char, long>();

            int muls = 0;

            for (char c = 'a'; c <= 'h'; c++)
            {
                registers.Add(c, 0);
            }

            for (long i = 0; i < input.Count();)
            {
                var order = input[i].Split(' ');
                string command = order[0];
                char register = order[1][0];
                long value = 0;
                long offset = 0;

                if (!long.TryParse(order[2], out value)) value = registers[order[2][0]];

                switch (command)
                {
                    case "set":
                        registers[register] = value;
                        break;
                    case "sub":
                        registers[register] -= value;
                        break;
                    case "mul":
                        registers[register] *= value;
                        muls++;
                        break;
                    case "jnz":
                        long x = 0;
                        if (!long.TryParse(order[1], out x)) x = registers[order[1][0]];
                        if (x != 0)
                        {
                            offset = value;
                        }
                        break;
                }

                if (offset == 0)
                {
                    i++;
                }
                else
                {
                    i += offset;
                    if (i < 0) i = 0;
                }
            }

            Console.WriteLine(muls);
        }

        public void day23_solve2()
        {
            var input = File.ReadAllLines(@"day23input.txt");
            Dictionary<char, long> registers = new Dictionary<char, long>();

            registers.Add('a', 1);

            for (char c = 'b'; c <= 'h'; c++)
            {
                registers.Add(c, 0);
            }

            registers['b'] = 57;
            registers['c'] = 57;

            registers['b'] *= 100;
            registers['b'] -= -100000;
            registers['c'] = registers['b'];
            registers['c'] -= -17000;

            do
            {
                registers['f'] = 1;
                registers['d'] = 2;
                registers['e'] = 2;

                for (registers['d'] = 2; registers['d'] < registers['b'];)
                {
                    if (registers['b'] % registers['d'] == 0)
                    {

                        registers['f'] = 0;
                        break;
                    }
                    registers['d'] += 1;
                }
                if (registers['f'] == 0)
                {
                    registers['h'] += 1;
                }
                registers['g'] = registers['b'] - registers['c'];
                registers['b'] -= -17;

            } while (registers['g'] != 0);

            Console.WriteLine(registers['h']);
        }
    }
}
