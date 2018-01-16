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
            long genAstart = 618;
            long genBstart = 814;
            long abin = 0;
            long bbin = 0;

            Queue<long> aq = new Queue<long>();
            Queue<long> bq = new Queue<long>();


            while (aq.Count() < 5000000 || bq.Count() < 5000000)
            {
                if (aq.Count() < 5000000)
                {
                    genAstart = Calc(genAstart, 16807);
                    if (genAstart % 4 == 0)
                    {
                        abin = genAstart & 0xFFFF;
                        aq.Enqueue(abin);
                    }
                }

                if (bq.Count() < 5000000)
                {
                    genBstart = Calc(genBstart, 48271);
                    if (genBstart % 8 == 0)
                    {
                        bbin = genBstart & 0xFFFF;
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
