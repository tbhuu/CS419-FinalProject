using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Homework6
{
    /*
     * Local Analyzer bases correlation analysis on only 
     * the local set of retrieved documents for a specific query.
     */
    class LocalAnalyzer
    {
        // Path to the document collection
        string docPath;

        // Path to the doc mapping file
        string mapPath;

        // List of stopwords
        private HashSet<string> stopwords;

        // Association Matrix
        Dictionary<string, Dictionary<string, int>> matrix;

        // Constructor
        public LocalAnalyzer(string docPath, string mapPath)
        {
            this.docPath = docPath;
            this.mapPath = mapPath;
            this.matrix = new Dictionary<string, Dictionary<string, int>>();
            // Read the file of stopwords
            stopwords = GetStopwords();
        }

        // Read all stopwords from file
        private HashSet<string> GetStopwords()
        {
            string[] stopwords = File.ReadAllText("..//..//Resources//stopwords_en.txt").Split();
            return new HashSet<string>(stopwords);
        }

        // Analyze the top 5 retrieved documents to compute the local Association Matrix
        public void Analyze(List<KeyValuePair<int, double>> searchResult)
        {
            matrix.Clear();
            for (int i = 0; i < Math.Min(4, searchResult.Count); ++i)
            {
                // Get the content of the document
                string document = GetDocContent(searchResult[i].Key);
                // Compute the frequency of each term in document
                Dictionary<string, int> terms = Preprocess(document);
                // Compute the correlation factor between every pair of terms in the document
                foreach (KeyValuePair<string, int> term in terms)
                {
                    if (!matrix.ContainsKey(term.Key))
                        matrix.Add(term.Key, new Dictionary<string, int>());
                    foreach (KeyValuePair<string, int> relatedterm in terms)
                    {
                        if (!matrix[term.Key].ContainsKey(relatedterm.Key))
                            matrix[term.Key].Add(relatedterm.Key, term.Value * relatedterm.Value);
                        matrix[term.Key][relatedterm.Key] += term.Value * relatedterm.Value;
                    }
                }
            }
        }

        // Preprocess the relevant documents
        // Return value: A dictionary matching each selected term with its frequency
        private Dictionary<string, int> Preprocess(string document)
        {
            // Tokenize the query
            MatchCollection words = Tokenizer.TokenizeDoc(document, @"([a-z]+'?[a-z]*)");

            // Get all terms and their frequencies in the document
            Dictionary<string, int> terms = new Dictionary<string, int>();
            foreach (var word in words)
            {
                string term = word.ToString().ToLower();
                if (stopwords.Contains(term))
                    continue;
                if (terms.ContainsKey(term))
                    ++terms[term];
                else
                    terms.Add(term, 1);
            }

            return terms;
        }

        // Get the content of a document
        private string GetDocContent(int docId)
        {
            using (BinaryReader mapReader = new BinaryReader(File.Open(mapPath, FileMode.Open)))
            {
                using (BinaryReader docReader = new BinaryReader(File.Open(docPath, FileMode.Open)))
                {
                    int low = 0;
                    int high = (Convert.ToInt32(mapReader.BaseStream.Length) / 12) - 1;
                    int value;

                    // Binary search
                    while (high >= low)
                    {
                        int mid = ((low + high) >> 1) * 12;
                        mapReader.BaseStream.Position = mid;
                        value = mapReader.ReadInt32();
                        if (docId.Equals(value))
                        {
                            string document = "";
                            int docPosition = mapReader.ReadInt32();
                            if (docPosition != -1)
                            {
                                docReader.BaseStream.Position = docPosition;
                                document = new string(docReader.ReadChars(mapReader.ReadInt32()));
                            }
                            return document;
                        }
                        else if (docId.CompareTo(value) > 0)
                            low = (mid / 12) + 1;
                        else
                            high = (mid / 12) - 1;
                    }
                }
            }

            return "";
        }

        // Get the list of related terms for query expansion
        public List<KeyValuePair<string, int>> GetRelatedTerms(string term)
        {
            if (!matrix.ContainsKey(term))
                return new List<KeyValuePair<string, int>>();
            List<KeyValuePair<string, int>> relatedTerms = matrix[term].ToList();
            // Sort the list in descending order of correlation factor
            relatedTerms.Sort((firstPair, nextPair) =>
            {
                return nextPair.Value.CompareTo(firstPair.Value);
            });
            return relatedTerms;
        }
    }
}
