using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    class day10
    {
        public day10()
        {
            day10_solve1();
            Console.WriteLine();
            day10_solve2();
        }
        public void day10_solve1()
        {
            string input = "63,144,180,149,1,255,167,84,125,65,188,0,2,254,229,24";
            var lengths = input.Split(',');

            List<int> hash = new List<int>();

            for (int i = 0; i < 256; i++)
            {
                hash.Add(i);
            }
            
            int index = 0;
            int skipSize = 0;

            foreach (string length in lengths)
            {
                int len = int.Parse(length);
                if (len + index > hash.Count())
                {
                    int extraIndex = len + index - hash.Count()-1;
                    var endlist = hash.GetRange(index, hash.Count() - index);
                    var startlist = hash.GetRange(0, extraIndex+1);
                    var combinedlist = endlist.Concat(startlist).ToList();
                    combinedlist.Reverse();
                    //add to end
                    hash.RemoveRange(index, hash.Count() - index);
                    hash.AddRange(combinedlist.GetRange(0, endlist.Count()));
                    //remove and add to start
                    hash.RemoveRange(0, extraIndex+1);
                    hash.InsertRange(0, combinedlist.GetRange(combinedlist.Count()-(extraIndex+1), startlist.Count()));
                }
                else { 
                    var reverseThis = hash.GetRange(index, len);
                    reverseThis.Reverse();
                    hash.RemoveRange(index, len);
                    hash.InsertRange(index, reverseThis);
                }

                index = (len + index + skipSize) % hash.Count();
                skipSize += 1;

            }

            Console.WriteLine(hash[0] * hash[1]);

        }

        public void day10_solve2()
        {
            string input = "63,144,180,149,1,255,167,84,125,65,188,0,2,254,229,24";
            var lengths = new List<int>();

            foreach (char c in input)
            {
                lengths.Add((int)c);
            }

            string addthese = "17,31,73,47,23";
            lengths.AddRange(addthese.Split(',').Select(int.Parse).ToList());

            List<int> hash = new List<int>();

            for (int i = 0; i < 256; i++)
            {
                hash.Add(i);
            }

            int index = 0;
            int skipSize = 0;
            
            for (int i = 0; i < 64; i++)
            {

                foreach (int len in lengths)
                {
                    //int len = int.Parse(length);
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

            //Convert to hex
            foreach (int nonhex in DenseHash)
            {
                string hexValue = nonhex.ToString("X").PadLeft(2, '0');
                Console.Write(hexValue.ToLower());
            }
        }
    }
}
