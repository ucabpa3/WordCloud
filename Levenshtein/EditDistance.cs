using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levenshtein
{
    public class EditDistance
    {
        public Dictionary<string, Dictionary<string, int>> ComputeALLLevenshtein(List<string> words)
        {
            Dictionary<string, Dictionary<string, int>> allDists = new Dictionary<string, Dictionary<string, int>>();
            Dictionary<string, int> distances = new Dictionary<string, int>();

            foreach (string word in words)
            {
                distances = ComputeLevenshtein(word, words);
                allDists.Add(word, distances);

            }

            return allDists;

        }

        public Dictionary<string, int> GetShortestLevenshtein(string givenWord, List<string> words)
        {
            Dictionary<string, int> dists = new Dictionary<string, int>();

            foreach (string word in words)
            {
                dists.Add(word, LevenshteinDistance(givenWord, word));
            }

            dists = dists.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            var shortestElement = dists.ElementAt(1);
            int shortestDist = shortestElement.Value;

            for (int i = dists.Count - 1; i > 0; i--)
            {
                var item = dists.ElementAt(i);

                if (item.Value != shortestDist)
                {
                    dists.Remove(item.Key);
                }
            }

            return dists;
        
        }


        public Dictionary<string, int> ComputeLevenshtein(string givenWord, List<string> words)
        {
            Dictionary<string, int> dists = new Dictionary<string, int>();

            foreach (string word in words)
            {
                dists.Add(word, LevenshteinDistance(givenWord, word));
            }

            dists = dists.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            return dists;
        }

        public static int LevenshteinDistance(string s, string t)
        {
            int[,] d = new int[s.Length + 1, t.Length + 1];
            for (int i = 0; i <= s.Length; i++)
                d[i, 0] = i;
            for (int j = 0; j <= t.Length; j++)
                d[0, j] = j;
            for (int j = 1; j <= t.Length; j++)
                for (int i = 1; i <= s.Length; i++)
                    if (s[i - 1] == t[j - 1])
                        d[i, j] = d[i - 1, j - 1];  //no operation
                    else
                        d[i, j] = Math.Min(
                            d[i - 1, j] + 1,    //a deletion
                            d[i, j - 1] + 1     //an insertion
                            );
            return d[s.Length, t.Length];
        }
    }
}
