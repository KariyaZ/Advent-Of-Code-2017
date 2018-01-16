using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    class day9
    {
        public day9()
        {
            string lines = File.ReadAllText(@"day9input.txt");

            int groups = 0;
            int totalscore = 0;
            int totalgarbage = 0;
            bool garbage = false;

            for(int i = 0; i < lines.Length; i++)
            {
                char c = lines[i];
                
                if (garbage)
                {
                    if (c == '>' && garbage)
                    {
                        garbage = false;
                        continue;
                    }
                    if (c == '!' && garbage)
                    {
                        i++;
                        continue;
                    }
                    totalgarbage += 1;
                } else
                {
                    if (c == '<')
                    {
                        garbage = true;
                    }
                    if (c == '{')
                    {
                        groups += 1;
                    }
                    if (c == '}')
                    {
                        totalscore += groups;
                        groups -= 1;
                    }
                }
            }
            Console.WriteLine(totalscore);
            Console.WriteLine(totalgarbage);
        }
    }
}
