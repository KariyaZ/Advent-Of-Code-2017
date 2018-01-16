using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    class day25
    {
        public day25()
        {
            var state = 'A';
            List<int> list = new List<int>();
            list = Enumerable.Range(0, 30000000).Select(i => 0).ToList();
            var cursor = list.Count() / 2;

            for (int i = 0; i < 12208951; i++)
            {
                switch (state)
                {
                    case 'A':
                        if (list[cursor] == 0)
                        {
                            list[cursor] = 1;
                            cursor++;
                            state = 'B';
                        } else
                        {
                            list[cursor] = 0;
                            cursor--;
                            state = 'E';
                        }
                        break;
                    case 'B':
                        if (list[cursor] == 0)
                        {
                            list[cursor] = 1;
                            cursor--;
                            state = 'C';
                        } else
                        {
                            list[cursor] = 0;
                            cursor++;
                            state = 'A';
                        }
                        break;
                    case 'C':
                        if (list[cursor] == 0)
                        {
                            list[cursor] = 1;
                            cursor--;
                            state = 'D';
                        }
                        else
                        {
                            list[cursor] = 0;
                            cursor++;
                            state = 'C';
                        }
                        break;
                    case 'D':
                        if (list[cursor] == 0)
                        {
                            list[cursor] = 1;
                            cursor--;
                            state = 'E';
                        }
                        else
                        {
                            list[cursor] = 0;
                            cursor--;
                            state = 'F';
                        }
                        break;
                    case 'E':
                        if (list[cursor] == 0)
                        {
                            list[cursor] = 1;
                            cursor--;
                            state = 'A';
                        }
                        else
                        {
                            cursor--;
                            state = 'C';
                        }
                        break;
                    case 'F':
                        if (list[cursor] == 0)
                        {
                            list[cursor] = 1;
                            cursor--;
                            state = 'E';
                        }
                        else
                        {
                            cursor++;
                            state = 'A';
                        }
                        break;
                    default:
                        throw (new NotImplementedException());
                }
            }

            Console.WriteLine(list.Sum());
        }
    }
}
