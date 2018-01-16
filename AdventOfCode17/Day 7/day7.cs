using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    class day7
    {
        public day7()
        {
            var reports = File.ReadLines(@"day7input.txt").Select(line =>
            {
                char[] separator = { ' ' };
                var split = line.Split(separator, 4);
                return new { Name = split[0], Weight = split[1].TrimStart('(').TrimEnd(')'), Children = split.Length > 2 ? split[3].Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries) : null, split };
            });

            var nodes = reports.Select(n => new Disc { Name = n.Name, Weight = int.Parse(n.Weight), ChildrenString = n.Children }).ToList();
            nodes.ForEach(n => {
                n.Parent = nodes.FirstOrDefault(x => x.ChildrenString != null && x.ChildrenString.Contains(n.Name));
                n.Parent?.Children.Add(n);
            });


            int index = nodes.FindIndex(disc => disc.Parent == null);

            Disc root = nodes[index];
            Console.WriteLine(root.Name);

            //Second part


            foreach (Disc parent in nodes.Where(disc => disc.Children.Count() > 1))
            {
                var min = parent.Children.Min(c => c.TotalWeight());
                var max = parent.Children.Max(c => c.TotalWeight());
                var diff = max - min;

                if (diff == 0) continue;

                var countMin = parent.Children.Count(c => c.TotalWeight() == min);
                var countMax = parent.Children.Count(c => c.TotalWeight() == max);

                if (countMax >= countMin)
                {
                    var n = parent.Children.First(x => x.TotalWeight() == min);
                    var ow = n.Weight;
                    n.Weight += diff;

                    if (IsBalanced(nodes))
                    {
                        Console.WriteLine("New node weight: {0}", n.Weight);
                        break;
                    }
                    n.Weight = ow;
                }
                else
                {
                    var n = parent.Children.First(x => x.TotalWeight() == max);
                    var ow = n.Weight;
                    n.Weight -= diff;

                    if (IsBalanced(nodes))
                    {
                        Console.WriteLine("New node weight: {0}", n.Weight);
                        break;
                    }
                    n.Weight = ow;
                }
            }

        }
        static bool IsBalanced(List<Disc> nodes)
        {
            foreach (var node in nodes.Where(n => n.Children.Count > 1))
            {
                int min1 = node.Children.Min(c => c.TotalWeight());

                int max1 = node.Children.Max(c => c.TotalWeight());

                int diff = max1 - min1;

                if (diff != 0) return false;
            }
            return true;
        }
    }

}

public class Disc
{
    public string Name { get; set; }
    public int Weight { get; set; }
    public Disc Parent { get; set; }
    public List<Disc> Children { get; } = new List<Disc>();
    public string[] ChildrenString { get; set; }

    public int TotalWeight()
    {
        return Weight + Children.Sum(c => c.TotalWeight());
    }


}
