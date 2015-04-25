using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace Homework6
{
    public partial class CurveDrawer : UserControl
    {
        // Constructor
        public CurveDrawer()
        {
            InitializeComponent();
        }

        // Draw the precision-recall curve and save it to folder Curves
        public void Draw(List<double> precisions, List<double> recalls, string fileName)
        {
            // Clear old points
            prCurve.Series["Precision-Recall"].Points.Clear();
            // Add new points to series
            for (int i = 0; i < precisions.Count; ++i)
                prCurve.Series["Precision-Recall"].Points.AddXY(recalls[i], precisions[i]);
            // Save the image to png file
            prCurve.SaveImage(File.Create("..//..//Curves//" + fileName + ".png"), ChartImageFormat.Png);
        }
    }
}
