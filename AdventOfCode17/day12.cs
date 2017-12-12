using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    class day12
    {
        public day12()
        {
            int index = 0;
            var lines = File.ReadLines(@"day12input.txt");
            List<Node> nodelist = new List<Node>();
            foreach (var line in lines)
            {
                var data = line.Split(' ');
                var node = new Node(data[0]);
                nodelist.Add(node);
            }
            foreach (var line in lines)
            {
                var data = line.Split(' ');
                for (int i = 2; i < data.Count(); i++)
                {
                    var name = data[i];
                    nodelist[index].AddConnection(nodelist.First(n => n.Name == data[i].TrimEnd(',')));
                    nodelist.First(x => x.Name == data[i].TrimEnd(',')).AddConnection(nodelist[index]);
                }
                index++;
            }

            Console.WriteLine(nodelist.First(x => x.Name == "0").GetGroup().Count.ToString());

            int groups = 0;

            while (nodelist.Any())
            {
                foreach (Node ingroup in nodelist.First().GetGroup())
                {
                    nodelist.Remove(ingroup);
                }
                groups++;
            }

            Console.WriteLine("Group amount: " + groups);
        }
    }

    public class Node
    {
        public string Name { get; set; }
        public List<Node> Connections { get; set; } = new List<Node>();

        public Node(string name)
        {
            Name = name;
        }

        public void AddConnection(Node node)
        {
            if(!Connections.Contains(node))
            {
                Connections.Add(node);
            }    
        }

        private void GetGroup(List<Node> groupPrograms)
        {
            groupPrograms.Add(this);

            foreach (var c in Connections)
            {
                if (!groupPrograms.Contains(c))
                {
                    c.GetGroup(groupPrograms);
                }
            }
        }

        public List<Node> GetGroup()
        {
            var groupPrograms = new List<Node>();
            GetGroup(groupPrograms);
            return groupPrograms;
        }

    }
}
