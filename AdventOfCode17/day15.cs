using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    class day15
    {
        public day15()
        {
            day15_solve2();
        }

        public void day15_solve1()
        {
            int count = 0;
            long genAstart = 618;
            long genBstart = 814;

            for (int i = 0; i < 40000000; i++)
            {
                genAstart = Calc(genAstart, 16807);
                genBstart = Calc(genBstart, 48271);

                string abin = Convert.ToString(genAstart, 2).PadLeft(20, '0');
                string bbin = Convert.ToString(genBstart, 2).PadLeft(20, '0');

                //Console.WriteLine(abin);
                //Console.WriteLine(bbin);
                if (abin.Substring(abin.Length - 16) == bbin.Substring(bbin.Length - 16))
                {
                    count++;
                }
            }

            Console.WriteLine(count);
        }

        public long Calc(long input, int factor)
        {
            return (input * factor) % 2147483647;
        }



        public void day15_solve2()
        {
            int matches = 0;
            int pairs = 0;
            long genAstart = 618;
            long genBstart = 814;
            string abin = "";
            string bbin = "";

            Queue<string> aq = new Queue<string>();
            Queue<string> bq = new Queue<string>();


            while (aq.Count() < 5000000 || bq.Count() < 5000000)
            {
                if (aq.Count() < 5000000)
                {
                    genAstart = Calc(genAstart, 16807);
                    if (genAstart % 4 == 0)
                    {
                        abin = Convert.ToString(genAstart, 2).PadLeft(17, '0');
                        abin = abin.Substring(abin.Length - 16);
                        aq.Enqueue(abin);
                    }
                }

                if (bq.Count() < 5000000)
                {
                    genBstart = Calc(genBstart, 48271);
                    if (genBstart % 8 == 0)
                    {
                        bbin = Convert.ToString(genBstart, 2).PadLeft(17, '0');
                        bbin = bbin.Substring(bbin.Length - 16);
                        bq.Enqueue(bbin);
                    }
                }
            }

            while(aq.Any() && bq.Any())
            {
                if (aq.Dequeue() == bq.Dequeue())
                {
                    matches++;
                }
            }
            Console.WriteLine(matches);
        }
    }
}
