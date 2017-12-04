using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17
{
    public class day4
    {
        public day4()
        {
            int goodpasswords = day4_solve1();
            Console.WriteLine(goodpasswords);
            int goodpass2 = day4_solve2();
            Console.WriteLine(goodpass2);
        }
        
        public int day4_solve1()
        {
            string line;
            int goodpasswords = 0;
            System.IO.StreamReader file = new System.IO.StreamReader(@"day4input.txt");
            while ((line = file.ReadLine()) != null)
            {
                List<string> list = line.Split(' ').ToList();
                if (list.Count == list.Distinct().Count())
                {
                    goodpasswords += 1;
                }
            }

            file.Close();

            return goodpasswords;
        }

        public int day4_solve2()
        {
            string line;
            int goodpasswords = 0;
            bool goodone = false;
            System.IO.StreamReader file = new System.IO.StreamReader(@"day4input.txt");
            while ((line = file.ReadLine()) != null)
            {
                if (isAnagram(line)) goodpasswords += 1;
            }

            file.Close();

            return goodpasswords;
        }

        public bool isAnagram(string line)
        {
            List<string> list = line.Split(' ').ToList();
            if (list.Count == list.Distinct().Count())
            {
                foreach (string word in list.ToList())
                {
                    List<string> otherWords = list;
                    otherWords.Remove(word);
                    foreach (string word2 in otherWords)
                    {
                        if (word2.Length == word.Length)
                        {
                            string a1 = String.Concat(word.OrderBy(c => c));
                            string a2 = String.Concat(word2.OrderBy(c => c));

                            if (a1 == a2)
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            } else
            {
                return false;
            }
        }
    }
}
