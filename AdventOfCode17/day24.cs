using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    public class day24
    {
        public static Dictionary<int, int> Bridges = new Dictionary<int, int>();
        public day24()
        {
            var input = File.ReadAllLines(@"day24input.txt");
            int length = input.Length;

            int[,] line = new int[length, 2];
            int[] values = new int[length];

            Dictionary<int, List<int>> connections = new Dictionary<int, List<int>>();
            for (int i = 0; i < length; i++)
            {
                var tokens = input[i].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                line[i, 0] = int.Parse(tokens[0]);
                line[i, 1] = int.Parse(tokens[1]);

                if (!connections.ContainsKey(line[i, 0])) {
                    connections.Add(line[i, 0], new List<int> { i });
                }

                if (!connections.ContainsKey(line[i, 1]))
                {
                    connections.Add(line[i, 1], new List<int> { i });
                }

                connections[line[i, 0]].Add(i);
                connections[line[i, 1]].Add(i);
                values[i] = line[i, 0] + line[i, 1];
            }

            bool[] used = new bool[length];

            int strongest = Recur(used,line,connections,values,0,0,length,0);

            int longest = 0;
            int LongestAndStrongest = 0;

            foreach (var bridge in Bridges)
            {
                if (longest < bridge.Key)
                {
                    longest = bridge.Key;
                    LongestAndStrongest = bridge.Value;
                }
            }

            Console.WriteLine("part1: {0}, part2: {1}", strongest, LongestAndStrongest);
        }

        public static int Recur(
            bool[] used,
            int[,] input,
            Dictionary<int, List<int>> connections,
            int[] values,
            int prevConnectorValue,
            int total,
            int length,
            int bridgeLength)
        {

            int bridgesum = total;

            foreach (var component in connections[prevConnectorValue])
            {
                if (used[component])
                {
                    continue;
                }

                total += values[component];
                used[component] = true;

                if (!Bridges.ContainsKey(bridgeLength))
                {
                    Bridges.Add(bridgeLength, 0);
                }

                Bridges[bridgeLength] = Math.Max(Bridges[bridgeLength], total);

                int connector;
                if (input[component, 0] == prevConnectorValue)
                {
                    connector = 1;
                }
                else
                {
                    connector = 0;
                }

                var newBridgesum = Recur(used, input, connections, values, input[component, connector], total, length, bridgeLength + 1);

                bridgesum = Math.Max(newBridgesum, bridgesum);

                used[component] = false;
                total -= values[component];
            }

            return bridgesum;
        }

    }
}
