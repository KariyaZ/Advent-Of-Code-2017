using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode17
{
    class day13
    {
        public day13()
        {
            var lines = File.ReadLines(@"day13input.txt");
            Dictionary<int, int> scanners = new Dictionary<int, int>();
            foreach (string line in lines)
            {
                var data = line.Split(':');
                scanners.Add(int.Parse(data[0]), int.Parse(data[1].TrimStart(' ')));
            }

            int LastScannerIndex = scanners.Last().Key;
            int severityNoDelay = 0;
            int severity = 0;
            int delay = -1;
            bool caught = true;

            while (caught)
            {
                caught = false;
                delay++;
                
                for (int i = 0; i <= LastScannerIndex; i++)
                {
                    if (scanners.ContainsKey(i))
                    {

                        if ((i+delay) % (2 * scanners[i] - 2) == 0)
                        {
                            //caught
                            caught = true;
                            severity += i * scanners[i];
                            if (delay != 0) break;
                        }
                    }
                    else continue;    
                }
                if (severityNoDelay == 0) severityNoDelay = severity;
            }

            Console.WriteLine(severityNoDelay);
            Console.WriteLine(delay);
        }
    }
}
