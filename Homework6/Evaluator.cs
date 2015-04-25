using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework6
{
    // Evaluator class to evaluate a given search engine
    class Evaluator
    {
        // The Search Engine to be evaluated
        SearchEngine se;

        // The set of queries
        List<string> queries = new List<string>();

        // The ground truth
        Dictionary<string, HashSet<int>> truth = new Dictionary<string, HashSet<int>>();

        // Constructor
        public Evaluator(SearchEngine se, string queryPath, string truthPath)
        {
            this.se = se;
            LoadQuery(queryPath);
            LoadGroundTruth(truthPath);
        }

        // Load the queries for evaluation
        private void LoadQuery(string queryPath)
        {
            string data = "";
            using (StreamReader sr = new StreamReader(queryPath))
            {
                while (!sr.EndOfStream)
                {
                    data = sr.ReadLine();
                    if (data.Contains("<num>"))
                        queries.Add(data.Substring(14));
                    else if (data.Contains("<desc>"))
                        queries.Add(sr.ReadLine());
                }
            }
        }

        // Load the ground truth
        private void LoadGroundTruth(string truthPath)
        {
            string data = "";
            string queryId;
            int docId;
            using (StreamReader sr = new StreamReader(truthPath))
            {
                while (!sr.EndOfStream)
                {
                    data = sr.ReadLine();
                    queryId = data.Split()[0];
                    docId = Convert.ToInt32(data.Split()[1]);
                    if (!truth.ContainsKey(queryId))
                        truth.Add(queryId, new HashSet<int>());
                    truth[queryId].Add(docId);
                }
            }
        }

        // Evaluate normal search capability of the engine
        public void EvaluateNormalSearch()
        {
            Console.WriteLine("NORMAL SEARCH EVALUAION");
            // Store the precision and recall at each recall point
            List<double> precisions = new List<double>();
            List<double> recalls = new List<double>();
            // R-Precision
            double r_precision = 0;
            // F-Measure
            double f_measure;
            // Average precision
            double ap = 0;
            // Mean Average Precision
            double map = 0;
            // User control for drawing curves
            CurveDrawer curveDrawer = new CurveDrawer();

            // Execute the queries to evaluate Search engine
            for (int i = 0; i < queries.Count; i += 2)
            {
                Console.WriteLine("-Evaluate with query:\n    " + queries[i]);
                List<KeyValuePair<int, double>> result = se.Search(queries[i + 1]);

                // Calculate all evaluation measures
                CalculateAll(queries[i], result, out precisions, out recalls, out r_precision, out f_measure, out ap);

                // Draw the precision-recall curve
                curveDrawer.Draw(precisions, recalls, queries[i] + "_normal");

                // Display the R-Precision
                Console.WriteLine("    R-Precision = " + r_precision);

                // Display the F-Measure
                Console.WriteLine("    F-Measure = " + f_measure);

                // Calculate the MAP
                map += ap;
            }

            // Display the MAP
            Console.WriteLine("MAP = " + map / (queries.Count / 2));
            Console.WriteLine();
        }

        // Evaluate search with query expansion using Global Analysis
        public void EvaluateGlobalSearch()
        {
            Console.WriteLine("SEARCH USING GLOBAL ANALYSIS EVALUAION");
            // Store the precision and recall at each recall point
            List<double> precisions = new List<double>();
            List<double> recalls = new List<double>();
            // R-Precision
            double r_precision = 0;
            // F-Measure
            double f_measure;
            // Average precision
            double ap = 0;
            // Mean Average Precision
            double map = 0;
            // User control for drawing curves
            CurveDrawer curveDrawer = new CurveDrawer();

            // Execute the queries to evaluate Search engine
            for (int i = 0; i < queries.Count; i += 2)
            {
                Console.WriteLine("-Evaluate with query:\n    " + queries[i]);
                List<KeyValuePair<int, double>> result = se.SearchWithGlobalExpansion(queries[i + 1]);

                // Calculate all evaluation measures
                CalculateAll(queries[i], result, out precisions, out recalls, out r_precision, out f_measure, out ap);

                // Draw the precision-recall curve
                curveDrawer.Draw(precisions, recalls, queries[i] + "_global");

                // Display the R-Precision
                Console.WriteLine("    R-Precision = " + r_precision);

                // Display the F-Measure
                Console.WriteLine("    F-Measure = " + f_measure);

                // Calculate the MAP
                map += ap;
            }

            // Display the MAP
            Console.WriteLine("MAP = " + map / (queries.Count / 2));
            Console.WriteLine();
        }

        // Evaluate search with query expansion using Local Analysis
        public void EvaluateLocalSearch()
        {
            Console.WriteLine("SEARCH USING LOCAL ANALYSIS EVALUAION");
            // Store the precision and recall at each recall point
            List<double> precisions = new List<double>();
            List<double> recalls = new List<double>();
            // R-Precision
            double r_precision = 0;
            // F-Measure
            double f_measure;
            // Average precision
            double ap = 0;
            // Mean Average Precision
            double map = 0;
            // User control for drawing curves
            CurveDrawer curveDrawer = new CurveDrawer();

            // Execute the queries to evaluate Search engine
            for (int i = 0; i < queries.Count; i += 2)
            {
                Console.WriteLine("-Evaluate with query:\n    " + queries[i]);
                List<KeyValuePair<int, double>> result = se.SearchWithLocalExpansion(queries[i + 1]);

                // Calculate all evaluation measures
                CalculateAll(queries[i], result, out precisions, out recalls, out r_precision, out f_measure, out ap);

                // Draw the precision-recall curve
                curveDrawer.Draw(precisions, recalls, queries[i] + "_local");

                // Display the R-Precision
                Console.WriteLine("    R-Precision = " + r_precision);

                // Display the F-Measure
                Console.WriteLine("    F-Measure = " + f_measure);

                // Calculate the MAP
                map += ap;
            }

            // Display the MAP
            Console.WriteLine("MAP = " + map / (queries.Count / 2));
            Console.WriteLine();
        }

        // Calculate all the required evaluation measures
        private void CalculateAll(string queryId, List<KeyValuePair<int, double>> result, out List<double> precisions, 
            out List<double> recalls, out double r_precision, out double f_measure, out double ap)
        {
            // Count the number of relevance documents so far
            double relevanceCount = 0;
            // Initialize the measurements
            precisions = new List<double>();
            recalls = new List<double>();
            r_precision = 0;
            f_measure = 0;
            ap = 0;

            // Compare the result to the ground truth
            for (int j = 0; j < result.Count; ++j)
            {
                if (truth[queryId].Contains(result[j].Key))
                {
                    ++relevanceCount;
                    precisions.Add(relevanceCount / (j + 1));
                    recalls.Add(relevanceCount / truth[queryId].Count);
                    ap += precisions[precisions.Count - 1];
                }
                if (j + 1 == truth[queryId].Count)
                    r_precision = relevanceCount / (j + 1);
            }

            // Compute the R-Precision
            if (truth[queryId].Count > result.Count)
                r_precision = relevanceCount / truth[queryId].Count;

            // Compute the F-Measure
            double p = relevanceCount / result.Count;
            double r = relevanceCount / truth[queryId].Count;
            f_measure = (2 * p * r) / (p + r);

            // Compute the Mean Average Precision
            if (relevanceCount > 0)
                ap /= relevanceCount;
        }
    }
}
