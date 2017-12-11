using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    class day11
    {
        public day11()
        {
            string input = File.ReadAllText(@"day11input.txt");
            var directions = input.Split(',');

            int xdir = 0;
            int ydir = 0;

            int maxdist = 0;

            foreach (string dir in directions)
            {
                switch (dir)
                {
                    case "n":
                        ydir += 2;
                        break;
                    case "ne":
                        xdir += 1;
                        ydir += 1;
                        break;
                    case "se":
                        xdir += 1;
                        ydir -= 1;
                        break;
                    case "s":
                        ydir -= 2;
                        break;
                    case "sw":
                        xdir -= 1;
                        ydir -= 1;
                        break;
                    case "nw":
                        xdir -= 1;
                        ydir += 1;
                        break;
                    default:
                        throw new ArgumentException();
                }

                int dist = distance(xdir, ydir);
                if (dist > maxdist) maxdist = dist;
            }

            Console.WriteLine(distance(xdir, ydir));
            Console.WriteLine(maxdist);

        }

        public int distance(int x, int y)
        {
            //Diagonal steps so changes both X and Y distance
            int moveXY = Math.Abs(x);

            //Once diagonal moves "run out" move the rest of the way straight up/down
            int moveY = (Math.Abs(y) - moveXY) / 2;

            return moveXY + moveY;
        }
    }
}
