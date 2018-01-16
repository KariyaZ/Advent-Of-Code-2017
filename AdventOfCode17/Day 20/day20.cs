using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    class day20
    {
        public day20()
        {
            var lines = File.ReadAllLines(@"day20input.txt");
            List<Particle> particles = new List<Particle>();
            char[] splitters = new char[] { 'p', '=', '<', '>', ',', 'v', 'a', ' ' };

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var data = line.Split(splitters, StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToList();
                particles.Add(new Particle()
                {
                    id = i,
                    xPos = data[0],
                    yPos = data[1],
                    zPos = data[2],
                    xVel = data[3],
                    yVel = data[4],
                    zVel = data[5],
                    xAcc = data[6],
                    yAcc = data[7],
                    zAcc = data[8]
                });
            }

            Particle closest = particles[0];

            for (int j = 0; j < 1000; j++)
            {
                particles.ForEach(part => part.Advance());

                var newMin = particles.OrderBy(p => p.GetDistance()).First();
                if (newMin != closest)
                {
                    closest = newMin;
                }
            }

            Console.WriteLine(closest.id);

            day20_solve2();
        }

        public void day20_solve2()
        {
            var lines = File.ReadAllLines(@"day20input.txt");
            List<Particle> particles = new List<Particle>();
            char[] splitters = new char[] { 'p', '=', '<', '>', ',', 'v', 'a', ' ' };

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var data = line.Split(splitters, StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToList();
                particles.Add(new Particle()
                {
                    id = i,
                    xPos = data[0],
                    yPos = data[1],
                    zPos = data[2],
                    xVel = data[3],
                    yVel = data[4],
                    zVel = data[5],
                    xAcc = data[6],
                    yAcc = data[7],
                    zAcc = data[8]
                });
            }

            for (int j = 0; j < 1000; j++)
            {
                particles.ForEach(part => part.Advance());

                var collisions = particles.GroupBy(x => x.GetPosition()).Where(x => x.Count() > 1).ToDictionary(g => g.Key, g => g.ToList());
                if (collisions.Count() > 0)
                {
                    foreach (var c in collisions)
                    {
                        foreach (var bad in c.Value)
                        {
                            particles.Remove(bad);
                        }
                    }
                }
            }

            Console.WriteLine(particles.Count());
        }
    }

    public class Particle
    {
        public int id { get; set; }
        public long xPos { get; set; }
        public long yPos { get; set; }
        public long zPos { get; set; }

        public long xVel { get; set; }
        public long yVel { get; set; }
        public long zVel { get; set; }

        public long xAcc { get; set; }
        public long yAcc { get; set; }
        public long zAcc { get; set; }

        public void Advance()
        {
            xVel += xAcc;
            yVel += yAcc;
            zVel += zAcc;

            xPos += xVel;
            yPos += yVel;
            zPos += zVel;
        }

        public long GetDistance()
        {
            return Math.Abs(xPos) + Math.Abs(yPos) + Math.Abs(zPos);
        }

        public string GetPosition()
        {
            return string.Join(",", xPos, yPos, zPos);
        }
    }
}
