using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    public class day3
    {
        public day3()
        {
            int input = 361527;
            int steps1 = day3_solve1(input);
            Console.WriteLine(steps1);
            int value = day3_solve2(input);
            Console.WriteLine(value);

        }
        public int day3_solve1(int input)
        {        
            //Which "ring" it is in? We can find out by calculating bottom right corner until it is >= input. This is because
            //bottom right corner is always odd squares: 1,3,9,25...
            int ring = 0;
            //first corner (center)
            int corner = 1;
            while (corner * corner < input)
            {
                corner += 2;
                ring += 1;
            }
            //ring is the number of steps towards center, now just find out the amount of side steps
            int distfromcorner = Math.Abs(input - corner * corner);
            int counter = 0;
            while (distfromcorner >= corner)
            {
                distfromcorner -= corner - 1;
                counter += 1;
            }
            //Calculate the middle of the side where the input is and distance to it
            int truecorner = corner * corner;
            int distToMid = corner / 2;
            int distToNextMid = corner - 1;
            int truemid = truecorner - distToMid - distToNextMid * counter;

            int sidesteps = Math.Abs(input - truemid);

            int totalsteps = ring + sidesteps;

            return totalsteps;
        }
        public int day3_solve2(int input)
        {
            var spiral = new Dictionary<string, int>();
            int x = 0;
            int y = 0;
            int StepsToTake = 1;
            bool addSteps = false;

            //middle
            spiral["0.0"] = 1;

            int direction = 0;

            for (; ;)
            {
                for (var i = 0; i < StepsToTake; i++)
                {
                    //Go through the spiral
                    switch (direction)
                    {
                        case 0:
                            //Go right
                            x += 1;
                            break;
                        case 1:
                            //Go up
                            y += 1;
                            break;
                        case 2:
                            //Go left
                            x -= 1;
                            break;
                        case 3:
                            //Go down
                            y -= 1;
                            break;
                        default:
                            break;
                    }

                    //Calculate the value for the "node" based on neighbours
                    int sum = 0;
                    int value = 0;
                    
                    for (int xx = -1; xx < 2; xx++)
                    {
                        for (int yy = -1; yy < 2; yy++)
                        {
                            if (xx == 0 && yy == 0) continue;
                            else
                            {
                                if (spiral.TryGetValue(string.Format("{0}.{1}", x + xx, y + yy), out value)) sum += value;
                            }
                        }
                    }

                    //check if sum > input, otherwise put it in as a value
                    if (sum > input) return sum;
                    spiral[string.Format("{0}.{1}", x, y)] = sum;
                }
                //Change direction when going through the spiral
                direction = (direction + 1) % 4;
                //Every 2 runs the step count increases so 1,1,2,2,3,3.....
                if (addSteps) StepsToTake += 1;
                addSteps = !addSteps;
            }
        }
    }
}
