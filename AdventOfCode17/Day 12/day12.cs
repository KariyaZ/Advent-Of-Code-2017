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
                    nodelist[index].AddConnection(nodelist.First(n => n.Name == data[i].TrimEnd(',')));
                    nodelist.First(n => n.Name == data[i].TrimEnd(',')).AddConnection(nodelist[index]);
                }
                index++;
            }

            Console.WriteLine(nodelist.First(node => node.Name == "0").GetGroup().Count.ToString());

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
            this.Name = name;
        }

        public void AddConnection(Node node)
        {
            if(!this.Connections.Contains(node))
            {
                this.Connections.Add(node);
            }    
        }

        private void GetGroup(List<Node> NodesInGroup)
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

        public List<Node> GetGroup()
        {
            var NodesInGroup = new List<Node>();
            GetGroup(NodesInGroup);
            return NodesInGroup;
        }

    }
}
