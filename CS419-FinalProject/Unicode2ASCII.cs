using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CS419_FinalProject
{
    static class Unicode2ASCII
    {
        static Regex regA = new Regex("[àáạãảăắằặẵẳâấầậẫẩ]");
        static Regex regE = new Regex("[éèẹẻẽêềếệểễ]");
        static Regex regD = new Regex("[đ]");
        static Regex regI = new Regex("[íìịỉĩ]");
        static Regex regY = new Regex("[ýỳỵỷỹ]");
        static Regex regU = new Regex("[ùúụũủưừứựữử]");
        static Regex regO = new Regex("[óòọỏõơờớởỡợôốồộổỗ]");


        public static string Convert(string content) {
            string a = regA.Replace(content,"a");
            a = regE.Replace(a, "e");
            a = regD.Replace(a, "d");
            a = regI.Replace(a, "i");
            a = regY.Replace(a, "y");
            a = regU.Replace(a, "u");
            return regO.Replace(a, "o");
        }
    }
}
