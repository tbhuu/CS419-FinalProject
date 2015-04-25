using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Homework6
{
    class Program
    {
        static void Main(string[] args)
        {
            // The path of the document collection, queries and groundtruth
            string path = "..\\..\\Resources";

            // Ask if user wants to create new index or use old index
            Console.Write("Do you want to use old index from a previous run (y/n)? ");
            string createNewIndex = Console.ReadLine().ToLower();

            /* Search Engine */
            // Search engine measure similarity by cosine of the angle between documents and queries
            SearchEngine se;
            if (createNewIndex.Equals("n"))
            {
                /* Construct index from document collection */
                // Index construction using SPIMI
                Indexer indexer = new Indexer(new SPIMIndexer(path));
                int docCount = indexer.Index();

                se = new SearchEngine("..//..//SPIMI//index.txt", "..//..//SPIMI//index_map.txt", "..//..//SPIMI//index_length.txt", docCount, 
                    "..//..//Resources//ohsumed.87", "..//..//Resources//doc_map.txt");
            }
            else
            {
                se = new SearchEngine("..//..//Index//index.txt", "..//..//Index//index_map.txt", "..//..//Index//index_length.txt", 54710,
                    "..//..//Resources//ohsumed.87", "..//..//Resources//doc_map.txt");
            }

            /* Start evaluating */
            Console.WriteLine("--SEARCH ENGINE EVALUATION--");
            Evaluator evaluator = new Evaluator(se, path + "\\query.ohsu.1-63", path + "\\qrels.ohsu.batch.87");

            // Ask which functions of the search engine user wants to evaluate
            Console.WriteLine("What functions of the search engine do you want to evaluate? ");
            Console.WriteLine("  1: Search without query expansion");
            Console.WriteLine("  2: Search with Automatic Global Analysis");
            Console.WriteLine("  3: Search with Automatic Local Analysis");
            Console.Write("Answer: ");
            string type = Console.ReadLine();
            if (type.Equals("1"))
            {
                // Evaluate normal search
                evaluator.EvaluateNormalSearch();
            }
            else if (type.Equals("2"))
            {
                // Evaluate search with query expansion using Global Analysis
                evaluator.EvaluateGlobalSearch();
            }
            else
            {
                // Evaluate search with query expansion using Local Analysis
                evaluator.EvaluateLocalSearch();
            }
            Console.ReadLine();
        }
    }
}
