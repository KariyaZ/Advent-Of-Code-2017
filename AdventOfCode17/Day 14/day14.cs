using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    class day14
    {
        public day14()
        {
            string input = "jzgqcdpd";
            day14_solver(input);
        }

        public void day14_solver(string input)
        {
            var inputs = new List<string>();

            for (int i = 0; i < 128; i++)
            {
                inputs.Add(input + "-" + i);
            }

            var lengths = new List<List<int>>();

            foreach (string modinput in inputs) {
                var changedinput = new List<int>();
                foreach (char c in modinput)
                {
                    changedinput.Add((int)c);
                }

                string addthese = "17,31,73,47,23";
                changedinput.AddRange(addthese.Split(',').Select(int.Parse).ToList());

                lengths.Add(changedinput);
            }
            List<int> hash = new List<int>();
            List<string> knothashes = new List<string>();

            for (int i = 0; i < 256; i++)
            {
                hash.Add(i);
            }

            List<int> hashcopy = hash;



            foreach (List<int> modlengths in lengths)
            {
                hash = new List<int>();
                hash.AddRange(hashcopy);
                int index = 0;
                int skipSize = 0;

                for (int i = 0; i < 64; i++)
                {

                    foreach (int len in modlengths)
                    {
                        if (len + index > hash.Count())
                        {
                            int extraIndex = len + index - hash.Count() - 1;
                            var endlist = hash.GetRange(index, hash.Count() - index);
                            var startlist = hash.GetRange(0, extraIndex + 1);
                            var combinedlist = endlist.Concat(startlist).ToList();
                            combinedlist.Reverse();
                            //add to end
                            hash.RemoveRange(index, hash.Count() - index);
                            hash.AddRange(combinedlist.GetRange(0, endlist.Count()));
                            //remove and add to start
                            hash.RemoveRange(0, extraIndex + 1);
                            hash.InsertRange(0, combinedlist.GetRange(combinedlist.Count() - (extraIndex + 1), startlist.Count()));
                        }
                        else
                        {
                            var reverseThis = hash.GetRange(index, len);
                            reverseThis.Reverse();
                            hash.RemoveRange(index, len);
                            hash.InsertRange(index, reverseThis);
                        }

                        index = (len + index + skipSize) % hash.Count();
                        skipSize += 1;

                    }
                }

                //Create dense hash

                var DenseHash = new List<int>();

                for (var x = 0; x < hash.Count(); x += 16)
                {
                    int batch = 0;

                    for (int i = 0; i < 16; i++)
                    {
                        batch ^= hash[x + i];
                    }

                    DenseHash.Add(batch);
                }

                string knothash = "";
                //Convert to hex
                foreach (int nonhex in DenseHash)
                {
                    string hexValue = nonhex.ToString("X").PadLeft(2, '0');
                    knothash += hexValue.ToLower();
                }

                knothashes.Add(knothash);
            }

            

            //pitäisitoimia

            int used = 0;

            List<string> binaryrows = new List<string>();
            foreach (string knothash in knothashes)
            {
                string converted = "";
                foreach (char c in knothash)
                {
                    converted += Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0');
                }
                used += converted.Count(x => x == '1');
                binaryrows.Add(converted);
            }
            Console.WriteLine("used: " + used);

            day14_part2(binaryrows);
        }

        public void day14_part2(List<string> binaryrows)
        {
            List<day14node> nodes = new List<day14node>();
            int[,] array = new int[binaryrows.Count, binaryrows.Count];

            for (int i = 0; i < binaryrows.Count; i++)
            {
                for(int j=0; j < binaryrows[i].Length; j++)
                {
                    string message = binaryrows[i];
                    array[i,j] = int.Parse(message[j].ToString());
                    if (int.Parse(message[j].ToString()) == 1) {
                        nodes.Add(new day14node(i + "," + j));
                    }
                }
            }

            int size = array.GetLength(0);

            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    if (array[row, column] == 1)
                    {
                        var node = nodes.First(n => n.Name == row + "," + column);

                        if (row == 0)
                        {
                            if (array[row + 1, column] == 1)
                            {
                                int fix = row + 1;
                                node.AddConnection(nodes.First(n => n.Name == fix + "," + column));
                            }
                        }
                        if (row > 0 && row < size - 1)
                        {
                            if (array[row - 1, column] == 1)
                            {
                                int fix = row - 1;
                                node.AddConnection(nodes.First(n => n.Name == fix + "," + column));
                            }
                            if (array[row + 1, column] == 1)
                            {
                                int fix = row + 1;
                                node.AddConnection(nodes.First(n => n.Name == fix + "," + column));
                            }
                        }
                        if (row == size - 1)
                        {
                            if (array[row - 1, column] == 1)
                            {
                                int fix = row - 1;
                                node.AddConnection(nodes.First(n => n.Name == fix + "," + column));
                            }
                        }
                        
                        
                        if (column == 0)
                        {
                            if (array[row, column + 1] == 1)
                            {
                                int fix = column + 1;
                                node.AddConnection(nodes.First(n => n.Name == row + "," + fix));
                            }
                        }
                        if (column > 0 && column < size - 1)
                        {
                            if (array[row, column - 1] == 1)
                            {
                                int fix = column - 1;
                                node.AddConnection(nodes.First(n => n.Name == row + "," + fix));
                            }
                            if (array[row, column + 1] == 1)
                            {
                                int fix = column + 1;
                                node.AddConnection(nodes.First(n => n.Name == row + "," + fix));
                            }
                        }
                        if (column == size - 1)
                        {
                            if (array[row, column - 1] == 1)
                            {
                                int fix = column - 1;
                                node.AddConnection(nodes.First(n => n.Name == row + "," + fix));
                            }
                        }                          
                    }
                }
            }

            int groups = 0;

            while (nodes.Any())
            {
                foreach (day14node ingroup in nodes.First().GetGroup())
                {
                    nodes.Remove(ingroup);
                }
                groups++;
            }

            Console.WriteLine("Group amount: " + groups);
        }


    }

    public class day14node
    {
        public string Name { get; set; }
        public List<day14node> Connections { get; set; } = new List<day14node>();

        public day14node(string name)
        {
            this.Name = name;
        }

        public void AddConnection(day14node node)
        {
            if (!this.Connections.Contains(node))
            {
                this.Connections.Add(node);
            }
        }

        private void GetGroup(List<day14node> NodesInGroup)
        {
            NodesInGroup.Add(this);

            foreach (var node in this.Connections)
            {
                if (!NodesInGroup.Contains(node))
                {
                    node.GetGroup(NodesInGroup);
                }
            }
        }

        public List<day14node> GetGroup()
        {
            var NodesInGroup = new List<day14node>();
            GetGroup(NodesInGroup);
            return NodesInGroup;
        }
    }
}
