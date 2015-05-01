using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CS419_FinalProject
{
    class SearchEngineASCII : SearchEngine
    {
        public SearchEngineASCII(string indexPath, string mapPath, string lenPath, int docCount, string collectionPath) : base(indexPath, mapPath, lenPath, docCount, collectionPath)
        {
            this.localAnalyzer = new LocalAnalyzerASCII(collectionPath);
        }

        protected override Dictionary<string, int> Preprocess(string query)
        {
            query = Unicode2ASCII.Convert(query);
            // Tokenize the query
            MatchCollection words = Tokenizer.TokenizeDoc(query, @"[a-zA-Z]+");

            // Get all terms and their frequencies in the query
            Dictionary<string, int> terms = new Dictionary<string, int>();
            foreach (var word in words)
            {
                string term = word.ToString().ToLower();
                if (terms.ContainsKey(term))
                    ++terms[term];
                else
                    terms.Add(term, 1);
            }

            return terms;
        }
    }
}
