using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    class day16
    {
        public day16()
        {
            string input = File.ReadAllText(@"day16input.txt");
            var commands = input.Split(',');
            char[] programs = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p' };
            //char[] copy = new char[programs.Length];
            //Array.Copy(programs, copy, programs.Length);

            for (int repeat = 0; repeat < 1000000000%36; repeat++)
            {
                foreach (string command in commands)
                {
                    if (command[0] == 's')
                    {
                        string length = command.Substring(1);
                        var placeholder = new char[programs.Length];
                        for (int i = 0; i < programs.Length; i++)
                        {
                            placeholder[(i + int.Parse(length)) % placeholder.Length] = programs[i];
                        }
                        programs = placeholder;
                    }

                    else if (command[0] == 'x')
                    {
                        var indexes = command.Substring(1).Split('/').Select(int.Parse).ToList();
                        char temp = programs[indexes[0]];
                        programs[indexes[0]] = programs[indexes[1]];
                        programs[indexes[1]] = temp;
                    }

                    else if (command[0] == 'p')
                    {
                        var names = command.Substring(1).Split('/').Select(char.Parse).ToList();
                        int index1 = Array.IndexOf(programs, names[0]);
                        int index2 = Array.IndexOf(programs, names[1]);

                        char temp = programs[index1];
                        programs[index1] = programs[index2];
                        programs[index2] = temp;
                    }
                }

                
                if (repeat == 0)
                {
                    foreach (char program in programs)
                    {
                        Console.Write(program);
                    }
                }
                /*
                if (copy.SequenceEqual(programs))
                {
                    Console.WriteLine(repeat);
                }*/
            }

            Console.Write('\n');

            foreach (char program in programs)
            {
                Console.Write(program);
            }

        }

    }
}
