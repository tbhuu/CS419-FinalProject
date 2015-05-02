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
        private Indexer myIndexer2;
        private SearchEngine mySearchEngine2;

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
                && File.Exists(GlobalParameter.indexLengthPath)
                && File.Exists(GlobalParameter.indexPathvi)
                && File.Exists(GlobalParameter.indexMapPathvi)
                && File.Exists(GlobalParameter.indexLengthPathvi))
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

            mySearchEngine2 = new SearchEngineASCII(
                GlobalParameter.indexPathvi,
                GlobalParameter.indexMapPathvi,
                GlobalParameter.indexLengthPathvi,
                docCount,
                "..//..//Resources//");
        }

        public void BuildBothIndexerAndSearchEngine() {
            myIndexer = new Indexer(new SPIMIndexer(resourcePath));
            myIndexer2 = new Indexer(new SPIMIndexerViASCII(resourcePath));
            docCount = myIndexer.Index();

            mySearchEngine = new SearchEngine(
                GlobalParameter.indexPath,
                GlobalParameter.indexMapPath,
                GlobalParameter.indexLengthPath,
                docCount,
                "..//..//Resources//");

            myIndexer2.Index();            

            mySearchEngine2 = new SearchEngineASCII(
                GlobalParameter.indexPathvi,
                GlobalParameter.indexMapPathvi,
                GlobalParameter.indexLengthPathvi,
                docCount,
                "..//..//Resources//");
        }

        //public void BuildIndexer()
        //{
        //    myIndexer = new Indexer(new SPIMIndexer(resourcePath));
        //    myIndexer2 = new Indexer(new SPIMIndexerViASCII(resourcePath));
        //    docCount = myIndexer.Index();
        //    myIndexer2.Index();
        //}

        private void CreateTemporaryFolder()
        {
            //Only create new if folder doesnot exist
            Directory.CreateDirectory("SPIMI");
            Directory.CreateDirectory("Resources");
            Directory.CreateDirectory("Index");
        }

        private SearchEngine ChooseApproriateSearchEngine(string query)
        {
            if (query.Equals(Unicode2ASCII.Convert(query)))
                return this.mySearchEngine2;
            return this.mySearchEngine;
        }

        public List<SearchResult> SearchQuery(string query) {

            SearchEngine chosenSearchEngine = ChooseApproriateSearchEngine(query);
            // Search and output the result
            List<KeyValuePair<int, double>> result = chosenSearchEngine.Search(query);

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
            SearchEngine chosenSearchEngine = ChooseApproriateSearchEngine(query);
            // Search and output the result
            List<KeyValuePair<int, double>> result = chosenSearchEngine.ExpandQuery(query, items);

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
