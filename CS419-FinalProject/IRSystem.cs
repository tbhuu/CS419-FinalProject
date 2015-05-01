using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS419_FinalProject
{
    class IRSystem
    {
        private Indexer myIndexer;
        private SearchEngine mySearchEngine;
        string[] docPaths;

        private string resourcePath = GlobalParameter.resourcesPath;
        private int docCount = GlobalParameter.docCount;



        public IRSystem() {
            CreateTemporaryFolder();  
            docPaths = Directory.GetFiles(resourcePath);
        }

        public bool PreviousRunExist()
        {
            if (File.Exists(GlobalParameter.indexPath)
                && File.Exists(GlobalParameter.indexMapPath)
                && File.Exists(GlobalParameter.indexLengthPath))
                return true;
            return false;
        }

        public void BuildSearchEngine()
        {
            mySearchEngine = new SearchEngine(
                GlobalParameter.indexPath,
                GlobalParameter.indexMapPath,
                GlobalParameter.indexLengthPath,
                docCount,
                "..//..//Resources//");
        }

        public void BuildIndexer()
        {
            myIndexer = new Indexer(new SPIMIndexer(resourcePath));
            docCount = myIndexer.Index();
        }

        private void CreateTemporaryFolder()
        {
            //Only create new if folder doesnot exist
            Directory.CreateDirectory("SPIMI");
            Directory.CreateDirectory("Resources");
            Directory.CreateDirectory("Index");
        }

        public List<SearchResult> SearchQuery(string query) {
            // Search and output the result
            List<KeyValuePair<int, double>> result = mySearchEngine.Search(query);

            List<SearchResult> returnresult = new List<SearchResult>();
                
            if (result.Count > 0)
            {
                // Display the ranked list
                for (int i = 0; i < result.Count; ++i)
                {
                    returnresult.Add(new SearchResult(docPaths[result[i].Key],result[i].Value));
                }
            }
            else
                Console.WriteLine("Result: Not found!");
            return returnresult;
        }

        public List<SearchResult> ExpandQuery(string query, int[] items)
        {
            // Search and output the result
            List<KeyValuePair<int, double>> result = mySearchEngine.ExpandQuery(query, items);

            List<SearchResult> returnresult = new List<SearchResult>();

            if (result.Count > 0)
            {
                // Display the ranked list
                for (int i = 0; i < result.Count; ++i)
                {
                    returnresult.Add(new SearchResult(docPaths[result[i].Key], result[i].Value));
                }
            }
            else
                Console.WriteLine("Result: Not found!");
            return returnresult;
        }
    }
}
