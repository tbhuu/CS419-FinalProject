using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework6
{
    /*
     * Global Analyzer determines term similarity through 
     * a statistical analysis of the complete corpus.
     */
    class GlobalAnalyzer
    {
        // Path to the inverted index file
        string indexPath;

        // Path to the termId mapping file
        string mapPath;

        // Constructor
        public GlobalAnalyzer(string indexPath, string mapPath)
        {
            this.indexPath = indexPath;
            this.mapPath = mapPath;
        }

        /*
         * Analyze the corpus to compute the Association Matrix at row i,
         * which represents the correlations between the given term
         * with the vocabulary.
         * Return value: a sorted list of statistically most correlated terms for query expansion
         */
        public List<KeyValuePair<string, int>> Analyze(string term)
        {
            // Read the postings list of the given term i
            List<int> postings_list = GetPostingsList(term);
            // Buffer to read the postings list of term j
            SourceBuffer colBuffer = new SourceBuffer(Disk.DISKBLOCK_SIZE, "..//..//Index//index.txt");

            // Compute row i of the Association Matrix
            List<KeyValuePair<string, int>> row = ComputeRowOfMatrix(postings_list, colBuffer);

            row.Sort((firstPair, nextPair) =>
            {
                return nextPair.Value.CompareTo(firstPair.Value);
            });

            return row;
        }

        // Get the postings list of the given term in the inverted index
        private List<int> GetPostingsList(string term)
        {
            List<int> postings_list = new List<int>();
            using (BinaryReader mapReader = new BinaryReader(File.Open(mapPath, FileMode.Open)))
            {
                using (BinaryReader indexReader = new BinaryReader(File.Open(indexPath, FileMode.Open)))
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
                        {
                            // Read doc frequency of the given term
                            int df = indexReader.ReadInt32();
                            // Read the postings list of the given term
                            for (int i = 0; i < df; ++i)
                                postings_list.Add(indexReader.ReadInt32());
                            break;
                        }
                        else if (term.CompareTo(value) > 0)
                            low = (mid >> 2) + 1;
                        else
                            high = (mid >> 2) - 1;
                    }
                }
            }

            return postings_list;
        }

        // Compute a row of the Association Matrix
        private List<KeyValuePair<string, int>> ComputeRowOfMatrix(List<int> postings_list, SourceBuffer colBuffer)
        {
            List<KeyValuePair<string, int>> row = new List<KeyValuePair<string, int>>();
            while (!colBuffer.IsCompletelyRead())
            {
                int df1 = 0;
                // Read term j and its doc frequency
                int len = colBuffer.GetInt();
                string term = colBuffer.GetString(len);
                int df2 = colBuffer.GetInt();

                // Compute correlation factor between term i and term j
                int c = 0;
                int docId1 = -1, f1 = 0;
                int docId2 = -1, f2 = 0;
                while (df1 <= postings_list.Count && df2 >= 0)
                {
                    if (docId1 == docId2)
                    {
                        c += f1 * f2;
                        GetNextPosting1(postings_list, ref df1, ref docId1, ref f1);
                        GetNextPosting2(colBuffer, ref df2, ref docId2, ref f2);
                    }
                    else if (docId1 < docId2)
                        GetNextPosting1(postings_list, ref df1, ref docId1, ref f1);
                    else
                        GetNextPosting2(colBuffer, ref df2, ref docId2, ref f2);
                }
                if (df2 > 0)
                    colBuffer.GetBytes(df2 << 2);

                // Store the correlation factor
                if (c > 0)
                {
                    row.Add(new KeyValuePair<string, int>(term, c));
                }
            }

            return row;
        }

        // Get next posting in postings list
        private void GetNextPosting1(List<int> postings_list, ref int df1, ref int docId1, ref int f1)
        {
            if (df1 < postings_list.Count)
            {
                docId1 = postings_list[df1];
                f1 = postings_list[df1 + 1];
            }
            df1 += 2;
        }

        // Get next posting in colBuffer
        private void GetNextPosting2(SourceBuffer colBuffer, ref int df2, ref int docId2, ref int f2)
        {
            if (df2 > 0)
            {
                docId2 = colBuffer.GetInt();
                f2 = colBuffer.GetInt();
            }
            df2 -= 2;
        }
    }
}
