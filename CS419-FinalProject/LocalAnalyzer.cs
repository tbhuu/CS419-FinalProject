using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CS419_FinalProject
{
    /*
     * Local Analyzer bases correlation analysis on only 
     * the local set of retrieved documents for a specific query.
     */
    class LocalAnalyzer
    {
        // Path to the document collection
        protected string docPath;

        // List of stopwords
        protected HashSet<string> stopwords;

        // Association Matrix
        protected Dictionary<string, Dictionary<string, int>> matrix;
        
        protected virtual string regexstring {
            get {
                return @"[A-ZÀÁẠÃẢĂẮẰẶẴẲÂẤẦẬẪẨÉÈẸẺẼÊỀẾỆỂỄĐÍÌỊỈĨÝỲỴỶỸÙÚỤŨỦƯỪỨỰỮỬÓÒỌỎÕƠỜỚỞỠỢÔỐỒỘỔỖa-zàáạãảăắằặẵẳâấầậẫẩéèẹẻẽêềếệểễđíìịỉĩýỳỵỷỹùúụũủưừứựữửóòọỏõơờớởỡợôốồộổỗ]+";
            }
        }

        // Constructor
        public LocalAnalyzer(string docPath)
        {
            this.docPath = docPath;
            this.matrix = new Dictionary<string, Dictionary<string, int>>();
            // Read the file of stopwords
            stopwords = GetStopwords();
            //stopwords = new HashSet<string>();
        }

        // Read all stopwords from file
        protected virtual HashSet<string> GetStopwords()
        {
            IEnumerable<string> stopwords = File.ReadLines("..//..//Resources//Stopword//stopwords_vi.txt");
            return new HashSet<string>(stopwords);
        }

        public void Analyze(List<KeyValuePair<int, double>> searchResult, int[] items = null)
        {
            if (items == null) {
                AnalyzeTopResult(searchResult);
            }
            else
            {
                AnalyzeUserClicker(searchResult, items);
            }
        }

        //Analyse with items list by user clicker
        protected void AnalyzeUserClicker(List<KeyValuePair<int, double>> searchResult, int[] items)
        {
            matrix.Clear();
            if (items == null)
                return;

            for (int i = 0; i < items.Length; ++i)
            {
                // Get the content of the document
                string document = GetDocContent(searchResult[items[i]].Key);
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

        // Analyze the top 5 retrieved documents to compute the local Association Matrix
        protected void AnalyzeTopResult(List<KeyValuePair<int, double>> searchResult)
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
        protected Dictionary<string, int> Preprocess(string document)
        {
            // Tokenize the query
            MatchCollection words = Tokenizer.TokenizeDoc(document, regexstring);

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
        protected virtual string GetDocContent(int docId)
        {
            string content = "";
            string[] fileNames = Directory.GetFiles(docPath);
            using (StreamReader rd = new StreamReader(fileNames[docId]))
            {
                content = rd.ReadToEnd();
            }
            return content;
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
