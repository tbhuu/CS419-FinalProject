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
            return new HashSet<string>();
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
