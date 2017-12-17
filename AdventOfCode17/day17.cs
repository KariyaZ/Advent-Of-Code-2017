using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    class day17
    {
        public day17()
        {
            //input
            day17_solve2();
        }

        public void day17_solve1()
        {
            int forwardstep = 343;
            int index = 0;

            List<int> buffer = new List<int> { 0 };
            for (int i = 1; i < 2018; i++)
            {
                index = (index + forwardstep + 1) % buffer.Count;
                buffer.Insert(index, i);
            }

            Console.WriteLine(buffer[buffer.IndexOf(2017) + 1]);
        }

        public void day17_solve2()
        {
            int forwardstep = 343;
            int index = 0;
            int ValueAfterZero = 0;

            List<int> buffer = new List<int> { 0 };
           
            for (int i = 1; i <= 50000000; i++)
            {
                index = ((index + forwardstep) % i) + 1;
                if (index == 1)
                {
                    ValueAfterZero = i;
                }
            }

            Console.WriteLine(ValueAfterZero);
        }
    }
}
