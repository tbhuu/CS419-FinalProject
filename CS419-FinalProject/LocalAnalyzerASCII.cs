using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS419_FinalProject
{
    class LocalAnalyzerASCII: LocalAnalyzer
    {
        protected override string regexstring {
            get {
                return @"[a-zA-Z]+";
            }
        }

        protected override HashSet<string> GetStopwords()
        {
            IEnumerable<string> stopwords = File.ReadLines("..//..//Resources//Stopword//stopwords_vi.txt");

            string[] stopwordarray = stopwords.ToArray<string>();
            for (int i = 0; i < stopwordarray.Length; ++i)
            {
                stopwordarray[i] = Unicode2ASCII.Convert(stopwordarray[i]);
            }

            return new HashSet<string>(stopwordarray);
        }

        protected override string GetDocContent(int docId)
        {
            string content = "";
            string[] fileNames = Directory.GetFiles(docPath);
            using (StreamReader rd = new StreamReader(fileNames[docId]))
            {
                content = rd.ReadToEnd();
            }
            content = Unicode2ASCII.Convert(content);
            return content;
        }

        public LocalAnalyzerASCII(string docPath)
            : base(docPath)
        {

        }
    }
}
