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



            //string query = "";
            //string stop = "n";
            //string[] docPaths = Directory.GetFiles(resourcePath);
            //do
            //{
            //    // Ask user to input the query
            //    Console.Write("Query: ");
            //    //query = Console.ReadLine().ToLower();
            //    query = "Người trẻ và điện thoại di động: chuyện phức tạp";

            //    // Search and output the result
            //    List<KeyValuePair<int, double>> result = mySearchEngine.Search(query);
                
            //    if (result.Count > 0)
            //    {
            //        // Display the ranked list
            //        for (int i = 0; i < result.Count; ++i)
            //        {
            //            Console.Write("Document: " + docPaths[result[i].Key]);
            //            Console.WriteLine(" Rank value: " + result[i].Value);
            //        }
            //    }
            //    else
            //        Console.WriteLine("Result: Not found!");

            //    // Ask if user wants to stop
            //    Console.Write("Do you want to stop (y/n)? ");
            //    stop = Console.ReadLine().ToLower();
            //} while (!stop.Equals("y"));
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
