using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    class day8
    {
        Dictionary<string, int> registries = new Dictionary<string, int>();
        public day8()
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"day8input.txt");
            int AbsoluteMax = 0;
            int maxValue = 0;
            while ((line = file.ReadLine()) != null)
            {
                line.TrimEnd('\n');
                List<string> list = line.Split(' ').ToList();

                Already_Exists(list[0]);
                Already_Exists(list[4]);
                //Console.WriteLine(list.GetRange(4, 3)[2]);

                if (Is_Comparison_True(list.GetRange(4, 3)))
                {
                    Do_Operation(list.GetRange(0, 3));
                }

                maxValue = registries.Aggregate((l, r) => l.Value > r.Value ? l : r).Value;

                if (maxValue > AbsoluteMax)
                {
                    AbsoluteMax = maxValue;
                }

            }


            Console.WriteLine(maxValue);
            Console.WriteLine(AbsoluteMax);
        }
    
        public void Already_Exists(string register)
        {
            if (!registries.ContainsKey(register))
            {
                registries.Add(register, 0);
            }
        }
        public bool Is_Comparison_True(List<string> input)
        {
            string operand = input[1];
            int registervalue = registries[input[0]];
            int comparisonvalue = int.Parse(input[2]);

            switch (operand)
            {
                case ">":
                    return registervalue > comparisonvalue;
                case "<":
                    return registervalue < comparisonvalue;
                case ">=":
                    return registervalue >= comparisonvalue;
                case "<=":
                    return registervalue <= comparisonvalue;
                case "==":
                    return registervalue == comparisonvalue;
                case "!=":
                    return registervalue != comparisonvalue;
                default:
                    throw new NotImplementedException();
                    break;
            }
        }

        public void Do_Operation(List<string> input)
        {
            if (input[1] == "inc")
            {
                registries[input[0]] += int.Parse(input[2]);
            } else
            {
                registries[input[0]] -= int.Parse(input[2]);
            }
        }
    }
}
