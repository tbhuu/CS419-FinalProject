using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CS419_FinalProject
{
    class Program
    {
        // The path of the document collection, queries and groundtruth
        

        static SearchForm myForm;
        static Thread uiThread;

        

        static void Main(string[] args)
        {
            CreateSearchForm();            
        }

        private static void CreateSearchForm()
        {
            uiThread = new Thread(() =>
            {
                myForm = new SearchForm();
                myForm.ShowDialog();
            });
            uiThread.Start();
        }

    }
}
