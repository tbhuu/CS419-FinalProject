using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CS419_FinalProject
{
    // Tokenizing class
    class Tokenizer
    {
        // Tokenize the given document using a regular expression
        public static MatchCollection TokenizeDoc(string content, string regex)
        {
            return Regex.Matches(content, @regex, RegexOptions.IgnoreCase);
        }
    }
}
