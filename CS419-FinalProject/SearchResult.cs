using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS419_FinalProject
{
    public class SearchResult
    {
        public string fileName;
        public double relevantValue;
        public SearchResult(string filename, double relevantvalue) {
            this.fileName = filename;
            this.relevantValue = relevantvalue;
        }
    }
}
