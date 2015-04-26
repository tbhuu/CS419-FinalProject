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
    // Store disk information
    class Disk
    {
        // Assume that disk block size is 256KB, i.e. we write 256KB of data at a time
        public static int DISKBLOCK_SIZE = 262144;
    }

    // Indexing class
    class Indexer
    {
        // Indexing Method
        IndexingMethod method;

        // Constructor
        public Indexer(IndexingMethod method)
        {
            // Indexing method: BSBI or SPIMI
            this.method = method;
        }

        // Perform indexing: read documents, remove stopwords, and create inverted index
        public int Index()
        {
            return method.Index();
        }
    }

    // Interface for Index Construction Methods
    interface IndexingMethod
    {
        // Construct inverted index
        int Index();
    }

    // Strategy 2: Single-pass in-memory indexing
    class SPIMIndexer : IndexingMethod
    {
        // Path of the document collection
        private string path;

        // List of stopwords
        private HashSet<string> stopwords;

        // Constructor
        public SPIMIndexer(string path)
        {
            this.path = path;
            // Read the file of stopwords
            stopwords = new HashSet<string>();
            //stopwords = GetStopwords();
        }

        // Read all stopwords from file
        private HashSet<string> GetStopwords()
        {
            string[] stopwords = File.ReadAllText(path + "//stopwords_en.txt").Split();
            return new HashSet<string>(stopwords);
        }

        // Perform indexing using SPIMI
        // All files generated during the indexing process are stored in SPIMI folder
        // Return value: the inverted index of the collection
        public int Index()
        {
            Console.WriteLine("Index Construction using Single-pass in-memory Indexing");

            // Initialize the dictionary
            Dictionary<string, List<int>> dict = new Dictionary<string, List<int>>(20011);
            // Count the size (in bytes) of the values (term, docId, frequency) added to dictionary and postings lists
            int count = 0;
            // Count the number of blocks written to disk
            int blockId = 0;

            // Delete old files
            string[] filePaths = Directory.GetFiles("..//..//SPIMI//");
            foreach (string filePath in filePaths)
                File.Delete(filePath);

            // Count the total number of documents
            int docId = 0;
            // Read and process all documents
            foreach (string docPath in Directory.GetFiles(path))
            {
                using (StreamReader sr = new StreamReader(docPath))
                {
                    string content = sr.ReadToEnd();
                    if (content != null)
                        SPIMI_Invert(content, docId, ref dict, ref count, "SPIMI_", ref blockId);
                    ++docId;
                }
            }
            // Write the remaining postings lists
            if (dict.Count > 0)
            {
                Console.WriteLine("Block " + blockId.ToString() + ": Finished!");
                List<string> sortedTerm = dict.Keys.ToList();
                sortedTerm.Sort();
                WriteBlockToDisk(sortedTerm, dict, "SPIMI_" + blockId.ToString());
                ++blockId;
            }

            // Merge all blocks
            Console.WriteLine("Merging blocks...");
            MergeBlocks("SPIMI_", blockId, "..//..//SPIMI//index", docId);

            Console.WriteLine("Inverted index successfully constructed.");
            return docId;
        }

        // Add tokens to dictionary and postings lists
        private void SPIMI_Invert(string content, int docId, ref Dictionary<string, List<int>> dict, ref int count, string outFileName, ref int blockId)
        {
            // Tokenize the document
            MatchCollection words = Tokenizer.TokenizeDoc(content, @"[A-ZÀÁẠÃẢĂẮẰẶẴẲÂẤẦẬẪẨÉÈẸẺẼÊỀẾỆỂỄĐÍÌỊỈĨÝỲỴỶỸÙÚỤŨỦƯỪỨỰỮỬÓÒỌỎÕƠỜỚỞỠỢÔỐỒỘỔỖa-zàáạãảăắằặẵẳâấầậẫẩéèẹẻẽêềếệểễđíìịỉĩýỳỵỷỹùúụũủưừứựữửóòọỏõơờớởỡợôốồộổỗ]+");

            // Add tokens to dictionary and postings lists
            foreach (var word in words)
            {
                string term = word.ToString().ToLower();
                // Remove stopwords
                if (stopwords.Contains(term))
                    continue;
                // Add term to dictionary
                if (!dict.ContainsKey(term))
                {
                    dict.Add(term, new List<int>(200));
                    count += term.Length << 1;
                }
                // Add posting directly to its postings list
                bool found = false;
                for (int i = 0; i < dict[term].Count; i += 2)
                {
                    // If docId already exist, increase the term frequency in doc
                    if (dict[term][i] == docId)
                    {
                        ++dict[term][i + 1];
                        found = true;
                        break;
                    }
                }
                // If docId does not exist, add it to postings list
                if (!found)
                {
                    dict[term].Add(docId);
                    dict[term].Add(1);
                    count += 8;
                }
            }
            // We assume that only 1-2MB of memory is available
            if (count >= 1048576)
            {
                Console.WriteLine("Block " + blockId.ToString() + ": Finished!");
                // Sort terms
                List<string> sortedTerm = dict.Keys.ToList();
                sortedTerm.Sort();
                // Write block to disk
                WriteBlockToDisk(sortedTerm, dict, outFileName + blockId.ToString());
                // Clear dictionary and increase block count
                dict.Clear();
                count = 0;
                ++blockId;
            }
        }

        // Write the index of a block to disk in binary
        private void WriteBlockToDisk(List<string> sortedTerm, Dictionary<string, List<int>> dict, string fileName)
        {
            // Write binary file in an well-defined format
            using (BinaryWriter writer = new BinaryWriter(File.Open("..//..//SPIMI//" + fileName + ".txt", FileMode.Create)))
            {
                foreach (string term in sortedTerm)
                {
                    // Format of an index: term length (4 bytes) + term (x bytes) + doc frequency (4 bytes) + postings lists
                    // Format of postings lists: docId1 (4 bytes) + term frequency (4 bytes) + docId2 (4 bytes) + ...
                    byte[] block = new byte[8 + (term.Length << 1) + (dict[term].Count << 2)];
                    // Convert term length
                    BitConverter.GetBytes(term.Length).CopyTo(block, 0);
                    // Convert term
                    for (int i = 0; i < term.Length; ++i)
                        BitConverter.GetBytes(term[i]).CopyTo(block, (i << 1) + 4);
                    // Convert doc frequency
                    BitConverter.GetBytes(dict[term].Count).CopyTo(block, 4 + (term.Length << 1));
                    // Convert postings list
                    int k = 8 + (term.Length << 1);
                    foreach (int value in dict[term])
                    {
                        BitConverter.GetBytes(value).CopyTo(block, k);
                        k += 4;
                    }
                    // Write block to disk
                    writer.Write(block);
                }
            }
        }

        // Write index of a block to disk in text
        private void WriteBlockTextToDisk(List<string> sortedTerm, Dictionary<string, List<int>> dict, string fileName)
        {
            using (StreamWriter sw = new StreamWriter("..//..//SPIMI//" + fileName + "_text.txt"))
            {
                foreach (string term in sortedTerm)
                {
                    sw.WriteLine(term.Length + " " + term + " " + dict[term].Count);
                    sw.WriteLine(string.Join(" ", dict[term].ToArray()));
                    sw.WriteLine();
                }
            }
        }

        // Merge all blocks in folder SPIMI
        private void MergeBlocks(string inFileName, int blockCount, string outFileName, int docCount)
        {
            /* Prepare neccessary variables */
            // Source buffers
            SourceBuffer[] srcBuffer = new SourceBuffer[blockCount];
            // Destination buffer for term-postings list
            DestinationBuffer desBuffer = new DestinationBuffer(Disk.DISKBLOCK_SIZE, outFileName + ".txt");
            // Destination buffer for termId-term mapping
            DestinationBuffer dictBuffer = new DestinationBuffer(Disk.DISKBLOCK_SIZE, outFileName + "_map.txt");
            int offset = 0;
            // Store the current term in each source buffer
            string[] terms = new string[blockCount];
            // Store the current doc frequency in each source buffer
            int[] df = new int[blockCount];
            // Length of all document vectors
            Dictionary<int, double> length = new Dictionary<int, double>(docCount);
            
            for (int i = 0; i < blockCount; ++i)
            {
                // Initialize each source buffer
                srcBuffer[i] = new SourceBuffer(Disk.DISKBLOCK_SIZE, "..//..//SPIMI//" + inFileName + i.ToString() + ".txt");
                // Initialize the current term in each buffer
                terms[i] = "";
            }

            /* Starting merging */
            while (true)
            {
                string minTerm = "";
                bool allPortionsConsumed = true;
                // Iterate through all source buffers to find the smallest term
                for (int i = 0; i < blockCount; ++i)
                {
                    // If the current block is completely consumed, ignore this buffer
                    if (srcBuffer[i].IsCompletelyRead())
                        continue;
                    // This block is not completely consumed
                    allPortionsConsumed = false;
                    // Get the current term
                    if (terms[i].Equals(""))
                    {
                        int len = srcBuffer[i].GetInt();
                        terms[i] = srcBuffer[i].GetString(len);
                    }
                    // Find lexicographically smallest term
                    if (minTerm.Equals("") ||
                        minTerm.CompareTo(terms[i]) > 0)
                        minTerm = terms[i];
                }
                // Stop if all portions have been merged
                if (allPortionsConsumed)
                {
                    // Write the remaining data on destination buffer to disk
                    desBuffer.WriteToDiskOnRequest();
                    break;
                }
                /* Store the smallest term's postings list to destination buffer */
                // Store the term
                desBuffer.Store(BitConverter.GetBytes(minTerm.Length));
                for (int i = 0; i < minTerm.Length; ++i)
                    desBuffer.Store(BitConverter.GetBytes(minTerm[i]));
                // Store the offset
                dictBuffer.Store(BitConverter.GetBytes(offset));
                offset += 4 + (minTerm.Length << 1);
                // Store doc frequency
                int docFrequency = 0;
                for (int i = 0; i < blockCount; ++i)
                {
                    if (terms[i] == minTerm)
                    {
                        df[i] = srcBuffer[i].GetInt();
                        docFrequency += df[i];
                    }
                }
                offset += 4 + (docFrequency << 2);
                desBuffer.Store(BitConverter.GetBytes(docFrequency));
                // Store postings list
                for (int i = 0; i < blockCount; ++i)
                {
                    if (terms[i] == minTerm)
                    {
                        double idf = 1 + Math.Log10(docCount / (df[i] >> 1));
                        for (int j = 0; j < (df[i] >> 1); ++j)
                        {
                            int docId = srcBuffer[i].GetInt();
                            int f = srcBuffer[i].GetInt();
                            double tf_idf = (1 + Math.Log10(f))*idf;
                            if (!length.ContainsKey(docId))
                                length.Add(docId, 0);
                            length[docId] += tf_idf*tf_idf;
                            desBuffer.Store(BitConverter.GetBytes(docId));
                            desBuffer.Store(BitConverter.GetBytes(f));
                        }
                        terms[i] = "";
                    }
                }
            }

            dictBuffer.WriteToDiskOnRequest();

            // Write the lengths of all document vectors to disk
            DestinationBuffer lenBuffer = new DestinationBuffer(Disk.DISKBLOCK_SIZE, outFileName + "_length.txt");
            foreach (KeyValuePair<int, double> docLen in length)
            {
                lenBuffer.Store(BitConverter.GetBytes(docLen.Key));
                lenBuffer.Store(BitConverter.GetBytes(docLen.Value));
            }
            lenBuffer.WriteToDiskOnRequest();

            return;
        }
    }
}
