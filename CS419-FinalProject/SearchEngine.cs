using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CS419_FinalProject
{
    // Search engine
    class SearchEngine
    {
        // Path to the inverted index file
        protected string indexPath;

        // Path to the termId mapping file
        protected string mapPath;

        // Path to the file containing lengths of all document vectors
        protected string lenPath;

        // Number of documents in collection
        protected int docCount;

        // Length of document vectors in collection
        protected Dictionary<int, double> length;

        // Automatic Local Analyzer
        protected LocalAnalyzer localAnalyzer;

        // Constructor
        public SearchEngine(string indexPath, string mapPath, string lenPath, int docCount, string collectionPath)
        {
            this.indexPath = indexPath;
            this.mapPath = mapPath;
            this.lenPath = lenPath;
            this.docCount = docCount;
            this.length = new Dictionary<int, double>(docCount);
            GetVectorLength();
            this.localAnalyzer = new LocalAnalyzer(collectionPath);
        }

        // Get the lengths of the document vectors
        protected void GetVectorLength()
        {
            SourceBuffer srcBuffer = new SourceBuffer(Disk.DISKBLOCK_SIZE, lenPath);
            while (!srcBuffer.IsCompletelyRead())
            {
                length.Add(srcBuffer.GetInt(), srcBuffer.GetDouble());
            }
        }

        // Preprocess the query
        // Return value: A dictionary matching each selected term with its frequency in query
        protected virtual Dictionary<string, int> Preprocess(string query)
        {
            // Tokenize the query
            MatchCollection words = Tokenizer.TokenizeDoc(query, @"[A-ZÀÁẠÃẢĂẮẰẶẴẲÂẤẦẬẪẨÉÈẸẺẼÊỀẾỆỂỄĐÍÌỊỈĨÝỲỴỶỸÙÚỤŨỦƯỪỨỰỮỬÓÒỌỎÕƠỜỚỞỠỢÔỐỒỘỔỖa-zàáạãảăắằặẵẳâấầậẫẩéèẹẻẽêềếệểễđíìịỉĩýỳỵỷỹùúụũủưừứựữửóòọỏõơờớởỡợôốồộổỗ]+");

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

        // Search
        public List<KeyValuePair<int, double>> Search(string query)
        {
            // Preprocess the query
            Dictionary<string, int> terms = Preprocess(query);

            // Arrays storing the cosine scores and lengths of all documents
            Dictionary<int, double> scores = new Dictionary<int, double>(docCount);

            // Compute the cosine scores to determine the similarity between the query and documents
            List<KeyValuePair<int, double>> result = new List<KeyValuePair<int, double>>();
            using (BinaryReader mapReader = new BinaryReader(File.Open(mapPath, FileMode.Open)))
            {
                using (BinaryReader indexReader = new BinaryReader(File.Open(indexPath, FileMode.Open)))
                {
                    foreach (KeyValuePair<string, int> term in terms)
                    {
                        // Perform binary search to find the postings list of the current term
                        bool found = FindPosition(term.Key, mapReader, indexReader);
                        if (!found)
                            continue;
                        // Get the DF of the current term
                        int df = indexReader.ReadInt32() >> 1;
                        // Compute the IDF weight of the current term
                        double idf = 1 + Math.Log10(docCount / df);
                        // Compute the TF weight of the current term in query
                        double wQuery = ComputeTF(term.Value)*idf;
                        for (int i = 0; i < df; ++i)
                        {
                            int docId = indexReader.ReadInt32();
                            double wDoc = ComputeTF(indexReader.ReadInt32())*idf;
                            if (!scores.ContainsKey(docId))
                                scores.Add(docId, 0);
                            scores[docId] += wQuery * wDoc;
                        }
                    }
                    // Normalize all scores greater than zero and add the coresponding documents to the result list
                    foreach (KeyValuePair<int, double> score in scores)
                    {
                        result.Add(new KeyValuePair<int, double>(score.Key, score.Value / Math.Sqrt(length[score.Key])));
                    }
                }
            }
           
            // Sort the result in descending order of rank
            result.Sort((firstPair, nextPair) =>
            {
                return nextPair.Value.CompareTo(firstPair.Value);
            });

            return result;
        }

        public List<KeyValuePair<int, double>> ExpandQuery(string query, int[] items)
        {
            // Search with the query to get the top ranked retrieved documents
            List<KeyValuePair<int, double>> originalResult = Search(query);

            // Analyze the top ranked retrieved documents to compute the Association Matrix
            localAnalyzer.Analyze(originalResult, items);

            // Preprocess the query
            Dictionary<string, int> terms = Preprocess(query);

            // For each term in query, expand it with two most related terms
            foreach (KeyValuePair<string, int> term in terms)
            {
                List<KeyValuePair<string, int>> relatedTerms = localAnalyzer.GetRelatedTerms(term.Key);
                for (int i = 0, count = 0; i < relatedTerms.Count && count < 3; ++i)
                {
                    if (!terms.Keys.Contains(relatedTerms[i].Key)) { 
                        query += " " + relatedTerms[i].Key;
                        ++count;
                    }
                }
            }

            // Perform retrieval with the expanded query
            return Search(query);
        }

        // Search with Query expansion using Automatic Local Analysis
        // Apply Pseudo Feedback to determine the top relevant documents
        public List<KeyValuePair<int, double>> SearchWithLocalExpansion(string query)
        {
            // Search with the query to get the top ranked retrieved documents
            List<KeyValuePair<int, double>> originalResult = Search(query);

            // Analyze the top ranked retrieved documents to compute the Association Matrix
            localAnalyzer.Analyze(originalResult);

            // Preprocess the query
            Dictionary<string, int> terms = Preprocess(query);

            // For each term in query, expand it with two most related terms
            foreach (KeyValuePair<string, int> term in terms)
            {
                List<KeyValuePair<string, int>> relatedTerms = localAnalyzer.GetRelatedTerms(term.Key);
                for (int i = 0, count = 0; i < relatedTerms.Count && count < 3; ++i)
                {
                    if (!terms.Keys.Contains(relatedTerms[i].Key))
                    {
                        query += " " + relatedTerms[i].Key;
                        ++count;
                    }
                }
            }

            // Perform retrieval with the expanded query
            return Search(query);
        }

        // Find the position of the given term in the inverted index
        protected bool FindPosition(string term, BinaryReader mapReader, BinaryReader indexReader)
        {
            int low = 0;
            int high = (Convert.ToInt32(mapReader.BaseStream.Length) >> 2) - 1;

            // Binary search
            // After having found the tem, the reading head of indexReader is set to the term's postings list
            while (high >= low)
            {
                int mid = ((low + high) >> 1) << 2;
                mapReader.BaseStream.Position = mid;
                indexReader.BaseStream.Position = mapReader.ReadInt32();
                int len = indexReader.ReadInt32();
                string value = "";
                for (int i = 0; i < len; ++i)
                    value += BitConverter.ToChar(indexReader.ReadBytes(2), 0);
                if (term.Equals(value))
                    return true;
                else if (term.CompareTo(value) > 0)
                    low = (mid >> 2) + 1;
                else
                    high = (mid >> 2) - 1;
            }

            return false;
        }

        // Compute the TF value of a term in doc or query
        protected double ComputeTF(int f)
        {
            return 1 + Math.Log10(f);
        }
    }
}
