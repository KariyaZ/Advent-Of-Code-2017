using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    class day18
    {
        public day18()
        {
            var input = File.ReadLines(@"day18input.txt").ToList();

            var program_0 = new Program(input);
            var program_1 = new Program(input);

            program_1.registers.Add("p", 1);

            var Sent = 0;
            var found = false;

            while (!found)
            {
                program_0.Run();
                program_1.receive = program_0.send;
                program_0.send = new List<long>();
                program_1.Run();
                program_0.receive = program_1.send;
                Sent += program_1.send.Count();
                program_1.send = new List<long>();

                if (!program_0.receive.Any() && !program_1.receive.Any())
                {
                    Console.WriteLine(Sent);
                    found = true;
                }
            }
        }

        public void day18_solve1(IEnumerable<string> lines) {

            Dictionary<string, long> registers = new Dictionary<string, long>();
            Dictionary<string, long> SoundsPlayed = new Dictionary<string, long>();
            bool recover = false;

            for (int i = 0; i < lines.Count();)
            {
                if (recover)
                {
                    break;
                }
                string line = lines.ElementAt(i);
                var input = line.Split(' ');
                string command = input[0];
                long modifier = 0;
                long commandmod = 0;
                if (input.Length > 2)
                {
                    if (!long.TryParse(input[2], out modifier))
                    {
                        modifier = registers[input[2]];
                    }
                }

                if (!registers.ContainsKey(input[1]))
                {
                    registers.Add(input[1], 0);
                }

                switch (command)
                {
                    case "snd":
                        if (SoundsPlayed.ContainsKey(input[1]))
                        {
                            SoundsPlayed[input[1]] = registers[input[1]];
                        }
                        else
                        {
                            SoundsPlayed.Add(input[1], registers[input[1]]);
                        }
                        break;
                    case "set":
                        registers[input[1]] = modifier;
                        break;
                    case "add":
                        registers[input[1]] += modifier;
                        break;
                    case "mul":
                        registers[input[1]] *= modifier;
                        break;
                    case "mod":
                        registers[input[1]] %= modifier;
                        break;
                    case "rcv":
                        long rcvx = 0;
                        bool convertrcv = long.TryParse(input[1], out rcvx);
                        if (!convertrcv) rcvx = registers[input[1]];
                        if (rcvx != 0)
                        {
                            if (SoundsPlayed.ContainsKey(input[1]))
                            {
                                Console.WriteLine(SoundsPlayed[input[1]]);
                                recover = true;
                            }
                        }
                        break;
                    case "jgz":
                        long jgzx = 0;
                        bool convertjgz = long.TryParse(input[1], out jgzx);
                        if (!convertjgz) jgzx = registers[input[1]];
                        if (jgzx > 0) commandmod = modifier;
                        break;
                }
                if (commandmod == 0)
                {
                    i++;
                }
                else
                {
                    i += unchecked((int)commandmod);
                    if (i < 0) i = 0;
                }
            }
        }

    }

    public class Program
    {
        public List<long> send { get; set; }
        public List<long> receive { get; set; }
        public List<string> commands { get; set; }
        public int index { get; set; }
        public Dictionary<string, long> registers { get; set; }

        public Program(List<string> input)
        {
            commands = input;
            send = new List<long>();
            receive = new List<long>();
            registers = new Dictionary<string, long>();
            index = 0;
            registers = new Dictionary<string, long>();

        }

        public void Run()
        {
            while (true)
            {
                if (index >= commands.Count())
                {
                    return;
                }

                string input = commands[index++];
                var line = input.Split(' ');
                var command = line[0];

                long x = 0;
                
                if (!registers.ContainsKey(line[1]))
                {
                    registers.Add(line[1], 0);
                }
                
                if (line.Count() > 2)
                {
                    if (!long.TryParse(line[2], out x))
                    {
                        x = registers[line[2]];
                    }
                }

                switch (command)
                {
                    case "snd":
                        long y = 0;
                        if (!long.TryParse(line[1], out y)) y = registers[line[1]];
                        send.Add(y);
                        break;
                    case "set":
                        registers[line[1]] = x;
                        break;
                    case "add":
                        registers[line[1]] += x;
                        break;
                    case "mul":
                        registers[line[1]] *= x;
                        break;
                    case "mod":
                        registers[line[1]] %= x;
                        break;
                    case "rcv":
                        if (receive.Any())
                        {
                            registers[line[1]] = receive.First();
                            receive.RemoveAt(0);
                        }
                        else
                        {
                            index--;
                            return;
                        }

                        break;
                    case "jgz":
                        y = 0;
                        if (!long.TryParse(line[1], out y)) y = registers[line[1]];
                        if (y > 0)
                        {
                            var commandmod = x;

                            index--;
                            index += (int)commandmod;
                        }

                        break;
                }
            }
        }
    }
}
