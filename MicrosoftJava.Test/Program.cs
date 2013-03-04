using MicrosoftJava.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicrosoftJava.Test {
    class Program {
        static void Main(string[] args) {
            string path = "IApplicationDataContext.cs";
            var words = ParseFile(path);

            foreach (Word w in words) {
                Console.WriteLine(w);
            }

            Console.ReadKey();
        }

        public static IEnumerable<Word> ParseFile(string path) {
            var words = API.PublicAPI.ExtractTokens(path).ToList();

            return from w in words.Where(w => w != null)
                   group w by new { w.Type, w.Name } into g
                   select new Word(g.Key.Type, g.Key.Name, g.Count());
        }

    }
}
